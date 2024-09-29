using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;
using CompanyApp.Mapper.MapperService;
using CompanyApp.Identity;

namespace CompanyApp.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetProducts() {
        try {

            var products = await _dbContext.Products.ToListAsync();

            return Ok(products);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getProduct/{id}")]
    public async Task<IActionResult> GetProductById(byte id) {
        try {

            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(product);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProduct")]
    public async Task<IActionResult> AddProduct(ProductDto payloadProduct) {
        try {

            var newProduct = _mapper.Map<ProductDto, Product>(payloadProduct);

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(byte id, UpdateProductDto payloadProduct) {
        try {

            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Data Not Found");
            }

            _mapper.Map(payloadProduct, existingProduct);

            _dbContext.Products.Update(existingProduct);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProduct);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(byte id) {
        try {

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Data Not Found");
            }
            
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}