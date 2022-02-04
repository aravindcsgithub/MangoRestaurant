using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            if (product.ProductId > 0)
            {
                _dbContext.Products.Update(product);
            }
            else
            {
                _dbContext.Products.Add(product);
            }
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productID)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.ProductId == productID);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return true;
            }
        }

        public async Task<ProductDto> GetProductByID(int productID)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productID);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> lstProducts = await _dbContext.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(lstProducts);
        }
    }
}
