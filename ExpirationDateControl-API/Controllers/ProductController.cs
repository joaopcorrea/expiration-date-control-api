using ExpirationDateControl_API.Dtos;
using ExpirationDateControl_API.Models;
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
        public ObjectResult Create([FromBody] ProductDto productDto)
        {
            var product = ConvertProductDtoToModel(productDto);

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

        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromBody] ProductDto productDto)
        {
            var product = ConvertProductDtoToModel(productDto);

            var response = repository.Put(id, product);
            if (response == null)
            {
                throw new Exception("Não foi possível atualizar o produto");
            }
            else
            {
                return Created("", response);
            }
        }

        [HttpPatch("{id}")]
        public ObjectResult Patch(int id, [FromBody] ProductDto productDto)
        {
            var product = ConvertProductDtoToModel(productDto);

            var response = repository.Patch(id, product);
            if (response == null)
            {
                throw new Exception("Não foi possível atualizar o produto");
            }
            else
            {
                return Created("", response);
            }
        }

        [HttpDelete("{id}")]
        public ObjectResult Delete(int id)
        {
            var response = repository.Delete(id);
            if (response == null)
            {
                throw new Exception("Não foi possível remover o produto");
            }
            else
            {
                return Created("", response);
            }
        }

        private Product ConvertProductDtoToModel(ProductDto dto)
        {
            var model = new Product()
            {
                Barcode = dto.BarCode,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                CreateDate = dto.CreateDate
            };

            return model;
        }
    }
}
