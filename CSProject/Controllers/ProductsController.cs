using CSProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace CSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CSProjectContext _context;

        public ProductsController(CSProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetProductById")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Route("")]
        public IActionResult PostProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtRoute("GetProductById", new { id = product.ID }, product);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ID)
                return BadRequest();

            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
