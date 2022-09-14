using ExpirationDateControl_API.Dtos;
using ExpirationDateControl_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpirationDateControl_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        IProductRepository repository;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ObjectResult GetAll()
        {
            var products = new List<ProductDto>()
            {
                new ProductDto {
                    BarCode = "barcode",
                    CreateDate = DateTime.Now
                },
                new ProductDto {
                    BarCode = "barcode",
                    CreateDate = DateTime.Now
                }
            };

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ObjectResult GetById(int id)
        {
            var product = new ProductDto()
            {

                BarCode = "barcode",
                CreateDate = DateTime.Now

            };

            return Ok(product);
        }

        [HttpPost]
        public ObjectResult Create([FromBody] ProductDto product)
        {
            var response = repository.Create(product);
            if (response == null)
            {
                throw new Exception("Não foi possível adicionar o produto");
            }
            else
            {
                return Created("", response);
            }
        }
    }
}
