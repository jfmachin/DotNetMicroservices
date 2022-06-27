using AutoMapper;
using Catalog.API.Data;
using Catalog.API.Models.DTOs;
using Catalog.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace Catalog.API.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase {
        private readonly ICatalogContext catalogContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CatalogController(ICatalogContext catalogContext, ILogger<CatalogController> logger, IMapper mapper) {
            this.catalogContext = catalogContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() {
            var products = await catalogContext.Products.Find(_ => true).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(string id) {
            var product = await catalogContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null) {
                logger.LogError($"Product {id} not found");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category) {
            var products = await catalogContext.Products.Find(x => x.Category == category).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDTO productDTO) {
            var product = mapper.Map<Product>(productDTO);
            await catalogContext.Products.InsertOneAsync(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product) {
            await catalogContext.Products.ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product);
            return Ok(product);
        }

        [HttpDelete("{id:length(24)}", Name="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProduct(string id) {
            await catalogContext.Products.DeleteOneAsync(x => x.Id == id);
            return Ok();
        }
    }
}
