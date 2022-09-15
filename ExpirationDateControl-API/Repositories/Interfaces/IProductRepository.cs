using ExpirationDateControl_API.Models;

namespace ExpirationDateControl_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Product Create(Product product);
        public IQueryable<Product> GetAll(int page, int maxResults);
        public IQueryable<Product> GetByFilter(int page, int maxResults, Product product);
        public Product GetById(int id);
        public Product Put(int id, Product product);
        public Product Patch(int id, Product product);
        public int Delete(int id);
    }
}
