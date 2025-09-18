using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        private readonly IConnectionMultiplexer redis;
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken token;
        public IProductRepository ProductRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public ICustomerBasketRepository CustomerBasket { get; }

        public IAuth Auth { get; }  

        public UnitOfWork(AppDbContext context, IConnectionMultiplexer redis,
            IMapper mapper, IImageManagementService imageManagementService,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IGenerateToken token)
        {
            _context = context;
            _mapper = mapper;
            this.redis = redis;
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.token = token;
            _imageManagementService = imageManagementService;

            ProductRepository = new ProductRepository(_context, _mapper, _imageManagementService);
            CategoryRepository = new CategoryRepository(_context);
            PhotoRepository = new PhotoRepository(_context);
            CustomerBasket = new CustomerBasketRepository(redis);
            Auth = new AuthRepository(userManager, emailService, signInManager, token);
            
        }

    }
}
