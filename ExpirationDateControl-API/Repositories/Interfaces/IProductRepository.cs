using ExpirationDateControl_API.Dtos;

namespace ExpirationDateControl_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public ProductDto Create(ProductDto product);
    }
}
