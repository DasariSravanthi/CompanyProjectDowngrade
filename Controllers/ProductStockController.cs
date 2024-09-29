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
public class ProductStockController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductStockController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("allProductStocks")]
    public async Task<IActionResult> GetProductStocks() {
        try {

            var productStocks = await _dbContext.ProductStocks.Include(_ => _.ProductDetails).Include(_ => _.Sizes).ToListAsync();

            return Ok(productStocks);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getProductStock/{id}")]
    public async Task<IActionResult> GetProductStockById(Int16 id) {
        try {

            var productStock = await _dbContext.ProductStocks.Include(_ => _.ProductDetails).Include(_ => _.Sizes).FirstOrDefaultAsync(_ => _.ProductStockId == id);

            if (productStock == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(productStock);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProductStock")]
    public async Task<IActionResult> AddProductStock(ProductStockDto payloadProductStock) {
        try {

            var productDetailExists = await _dbContext.Set<ProductDetail>().AnyAsync(_ => _.ProductDetailId == payloadProductStock.ProductDetailId);
            if (!productDetailExists)
            {
                return BadRequest("Invalid ProductDetailId");
            }

            if (payloadProductStock.SizeId.HasValue) {
                var sizeExists = await _dbContext.Set<Size>().AnyAsync(_ => _.SizeId == payloadProductStock.SizeId);
                if (!sizeExists)
                {
                    return BadRequest("Invalid SizeId");
                }
            }

            var newProductStock = _mapper.Map<ProductStockDto, ProductStock>(payloadProductStock);

            await _dbContext.ProductStocks.AddAsync(newProductStock);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductStockById), new { id = newProductStock.ProductStockId }, newProductStock);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }  
    }

    [HttpPut("updateProductStock/{id}")]
    public async Task<IActionResult> UpdateProductStock(Int16 id, UpdateProductStockDto payloadProductStock) {
        try {

            var existingProductStock = await _dbContext.ProductStocks.FindAsync(id);
            if (existingProductStock == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadProductStock.ProductDetailId.HasValue) {
                var productDetailExists = await _dbContext.Set<ProductDetail>().AnyAsync(_ => _.ProductDetailId == payloadProductStock.ProductDetailId);
                if (!productDetailExists)
                {
                    return BadRequest("Invalid ProductDetailId");
                }
            }

            if (payloadProductStock.SizeId.HasValue) {
                var sizeExists = _dbContext.Set<Size>().Any(_ => _.SizeId == payloadProductStock.SizeId);
                if (!sizeExists)
                {
                    return BadRequest("Invalid SizeId");
                }
            }

            _mapper.Map(payloadProductStock, existingProductStock);

            _dbContext.ProductStocks.Update(existingProductStock);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProductStock);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteProductStock/{id}")]
    public async Task<IActionResult> DeleteProductStock(Int16 id) {
        try {

            var productDetail = await _dbContext.ProductStocks.FindAsync(id);
            if (productDetail == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.ProductStocks.Remove(productDetail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}