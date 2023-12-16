using CSProject.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CSProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CSProjectContext _context;

        public ProductsController(CSProjectContext context)
        {
            _context = context;
        }

        [HttpHead]
        [Route("{id:int}")]
        public IActionResult GetHead(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
                return NotFound();

            Response.Headers.Append("X-Product-Name", product.Name);

            return NoContent();
        }

        [HttpOptions]
        [Route("")]
        public IActionResult Options()
        {
            Response.Headers.Append("Access-Control-Allow-Origin", "*");
            Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, HEAD, OPTIONS");
            Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(product, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SaveChanges();

            return Ok(product);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetProducts()
        {
            var products = _context.Product.ToList();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetProductById")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Route("")]
        public IActionResult PostProduct(Product product)
        {
            _context.Product.Add(product);
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
            var product = _context.Product.Find(id);
            if (product == null)
                return NotFound();

            _context.Product.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
