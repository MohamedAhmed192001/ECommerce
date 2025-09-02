using AutoMapper;
using ECommerce.Core.DTOs.Products;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
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
    }
}
