using ExpirationDateControl_API.Context;
using ExpirationDateControl_API.Models;
using ExpirationDateControl_API.Repositories.Interfaces;

namespace ExpirationDateControl_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InMemoryContext _context;

        public ProductRepository(InMemoryContext inMemoryContext)
        {
            _context = inMemoryContext;
        }

        public Product Create(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }

        public int Delete(int id)
        {
            var product = _context.Find<Product>(id);

            if (product == null)
            {
                throw new Exception("Id inexistente");
            }

            _context.Remove(product);
            _context.SaveChanges();
            return id;
        }

        public IQueryable<Product> GetAll(int page, int maxResults)
        {
            var data = _context.Set<Product>().AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
            return data.Any() ? data : new List<Product>().AsQueryable();
        }

        public IQueryable<Product> GetByFilter(int page, int maxResults, Product product)
        {
            var data = _context.Set<Product>().AsQueryable()
                .Where(p => (product.Barcode == null || p.Barcode.Contains(product.Barcode)) &&
                            (product.Description == null || p.Description.Contains(product.Description)) &&
                            (product.CreateDate == null || product.CreateDate == p.CreateDate) &&
                            (product.Price == null || product.Price == p.Price) &&
                            (product.Quantity == null || product.Quantity == p.Quantity))
                .Skip((page - 1) * maxResults)
                .Take(maxResults);

            return data.Any() ? data : new List<Product>().AsQueryable();
        }

        public Product? GetById(int id)
        {
            return _context.Find<Product>(id);
        }

        public Product Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}
