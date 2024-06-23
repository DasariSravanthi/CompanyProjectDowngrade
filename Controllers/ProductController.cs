using Microsoft.AspNetCore.Mvc;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;
using CompanyApp.Mapper.MapperService;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductController(CompanyDbContext dbContext, AppMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("allProducts")]
    public ActionResult<IEnumerable<Product>> GetProducts() {

        var products = _dbContext.Products.ToList();

        return Ok(products);
    }

    [HttpGet("getProduct/{id}")]
    public ActionResult GetProductById(byte id) {

        var product = _dbContext.Products.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost("addProduct")]
    public ActionResult AddProduct(ProductDto payloadProduct) {

        var newProduct = _mapper.Map<ProductDto, Product>(payloadProduct);

        _dbContext.Products.Add(newProduct);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
    }

    [HttpPut("updateProduct/{id}")]
    public ActionResult UpdateProduct(byte id, UpdateProductDto payloadProduct) {

        var existingProduct = _dbContext.Products.Find(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadProduct, existingProduct);

        _dbContext.Products.Update(existingProduct);
        _dbContext.SaveChanges();

        return Ok(existingProduct);
    }

    [HttpDelete("deleteProduct/{id}")]
    public ActionResult DeleteProduct(byte id)
    {
        var product = _dbContext.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();

        return NoContent();
    }
}