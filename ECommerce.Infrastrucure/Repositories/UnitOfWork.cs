using AutoMapper;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
using ECommerce.Infrastructure.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        private readonly IConnectionMultiplexer redis;
        public IProductRepository ProductRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public ICustomerBasketRepository CustomerBasket { get; }

        public UnitOfWork(AppDbContext context, IConnectionMultiplexer redis, IMapper mapper, IImageManagementService imageManagementService)
        {
            _context = context;
            _mapper = mapper;
            this.redis = redis;
            _imageManagementService = imageManagementService;
            ProductRepository = new ProductRepository(_context, _mapper, _imageManagementService);
            CategoryRepository = new CategoryRepository(_context);
            PhotoRepository = new PhotoRepository(_context);
            CustomerBasket = new CustomerBasketRepository(redis);
        }


    }
}
