using ExpirationDateControl_API.Dtos;

namespace ExpirationDateControl_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public ProductDto Create(ProductDto product);
        public IQueryable<ProductDto> GetAll(int page, int maxResults);
        public ProductDto GetById(int id);
        public ProductDto Put(int id, ProductDto product);
        public ProductDto Patch(int id, ProductDto product);
        public int Delete(int id);
    }
}
