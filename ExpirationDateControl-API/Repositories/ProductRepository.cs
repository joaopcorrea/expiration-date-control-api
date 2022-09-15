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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetByFilter(int page, int maxResults, Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product Patch(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public Product Put(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
