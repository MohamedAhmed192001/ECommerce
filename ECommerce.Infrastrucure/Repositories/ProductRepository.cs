using AutoMapper;
using ECommerce.Core.DTOs.Products;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
using ECommerce.Core.Sharing;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }
        public async Task<IEnumerable<ViewProductDto>> GetAllAsync(ProductParams productParams)
        {
            // Base query
            var query = _context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();


            // Filtering by word
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in searchWords)
                {
                    var temp = $"%{word.ToLower()}%"; // prepare LIKE pattern outside query

                    query = query.Where(m =>
                        EF.Functions.Like(m.Name.ToLower(), temp) ||
                        EF.Functions.Like(m.Description.ToLower(), temp));
                }
            }


            // Filtering by category Id
            if (productParams.CategoryId.HasValue)
                query = query.Where(m => m.CategoryId == productParams.CategoryId);

            // Sorting
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query = productParams.Sort switch
                {
                    "PriceAce" => query.OrderBy(m => m.NewPrice),
                    "PriceDce" => query.OrderByDescending(m => m.NewPrice),
                    _ => query.OrderBy(m => m.Name),
                };
            }

            // Total Count
            productParams.TotatlCount = await query.CountAsync();

            // Pagination
            var products = await query
                .Skip(productParams.pageSize * (productParams.PageNumber - 1))
                .Take(productParams.pageSize)
                .ToListAsync();

            // Mapping
            var result = _mapper.Map<IEnumerable<ViewProductDto>>(products);

            return result;
        }
        public async Task<bool> AddAsync(AddProductDto productDTO)
        {
            if (productDTO == null) return false;

            var product = _mapper.Map<Product>(productDTO);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var ImagePath = await _imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }
            var FindProduct = await _context.Products.Include(m => m.Category)
                .Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == updateProductDTO.Id);

            if (FindProduct is null)
            {
                return false;
            }
            _mapper.Map(updateProductDTO, FindProduct);

            var FindPhoto = await _context.Photos.Where(m => m.ProductId == updateProductDTO.Id).ToListAsync();

            foreach (var item in FindPhoto)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Photos.RemoveRange(FindPhoto);

            var ImagePath = await _imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDTO.Id,
            }).ToList();

            await _context.Photos.AddRangeAsync(photo);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await _context.Photos.Where(m => m.ProductId == product.Id)
            .ToListAsync();
            foreach (var item in photo)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }


       
    }
}
