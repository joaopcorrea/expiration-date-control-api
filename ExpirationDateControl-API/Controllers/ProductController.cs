using ExpirationDateControl_API.Dtos;
using ExpirationDateControl_API.Models;
using ExpirationDateControl_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(typeof(IQueryable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Authorize]
        public ActionResult GetAll(int page = 1, int maxResults = 100)
        {
            var products = repository.GetAll(page, maxResults);
            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        [HttpPost("query")]
        [ProducesResponseType(typeof(IQueryable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetByFilter([FromBody] ProductDto productDto, int page = 1, int maxResults = 100)
        {
            var productModel = ConvertProductDtoToModel(productDto);
            var products = repository.GetByFilter(page, maxResults, productModel);
            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int id)
        {
            var product = repository.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public ActionResult Create([FromBody] ProductDto productDto)
        {
            var product = ConvertProductDtoToModel(productDto);

            var response = repository.Create(product);
            return Created("", response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public ActionResult Put(int id, [FromBody] ProductDto productDto)
        {
            var productInDb = repository.GetById(id);
            var productModel = ConvertProductDtoToModel(productDto, productInDb);

            if (productInDb == null)
            {
                var created = repository.Create(productModel);
                return Created("", created);
            }

            productModel.Id = id;
            var response = repository.Update(productModel);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Patch(int id, [FromBody] ProductDto productDto)
        {
            var productInDb = repository.GetById(id);
            if (productInDb == null)
                return NotFound();

            productDto.Barcode ??= productInDb.Barcode;
            productDto.Description ??= productInDb.Description;
            productDto.CreateDate ??= productInDb.CreateDate;
            productDto.Price ??= productInDb.Price;
            productDto.Quantity ??= productInDb.Quantity;

            var productModel = ConvertProductDtoToModel(productDto, productInDb);

            var response = repository.Update(productModel);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var productInDb = repository.GetById(id);
            if (productInDb == null)
                return NotFound();

            repository.Delete(id);
            return NoContent();
        }

        private Product ConvertProductDtoToModel(ProductDto dto, Product? model = null)
        {
            model ??= new Product();

            model.Barcode = dto.Barcode;
            model.Description = dto.Description;
            model.Price = dto.Price;
            model.Quantity = dto.Quantity;
            model.CreateDate = dto.CreateDate;

            return model;
        }
    }
}
