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
public class ProductDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductDetailController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductDetails")]
    public async Task<IActionResult> GetProductDetails() {
        try {

            var productDetails = await _dbContext.ProductDetails.Include(_ => _.Products).ToListAsync();

            return Ok(productDetails);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getProductDetail/{id}")]
    public async Task<IActionResult> GetProductDetailById(byte id) {
        try {

            var productDetail = await _dbContext.ProductDetails.Include(_ => _.Products).FirstOrDefaultAsync(_ => _.ProductDetailId == id);

            if (productDetail == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(productDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProductDetail")]
    public async Task<IActionResult> AddProductDetail(ProductDetailDto payloadProductDetail) {
        try {

            //Validate foreign key

            var productExists = await _dbContext.Set<Product>().AnyAsync(_ => _.ProductId == payloadProductDetail.ProductId);
            if (!productExists)
            {
                return BadRequest("Invalid ProductId");
            }

            var newProductDetail = _mapper.Map<ProductDetailDto, ProductDetail>(payloadProductDetail);

            await _dbContext.ProductDetails.AddAsync(newProductDetail);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductDetailById), new { id = newProductDetail.ProductDetailId }, newProductDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateProductDetail/{id}")]
    public async Task<IActionResult> UpdateProductDetail(byte id, UpdateProductDetailDto payloadProductDetail) {
        try {

            var existingProductDetail = await _dbContext.ProductDetails.FindAsync(id);
            if (existingProductDetail == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadProductDetail.ProductId.HasValue) {
                var productExists = await _dbContext.Set<Product>().AnyAsync(_ => _.ProductId == payloadProductDetail.ProductId);
                if (!productExists)
                {
                    return BadRequest("Invalid ProductId");
                }
            }

            _mapper.Map(payloadProductDetail, existingProductDetail);

            _dbContext.ProductDetails.Update(existingProductDetail);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProductDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteProductDetail/{id}")]
    public async Task<IActionResult> DeleteProductDetail(byte id) {
        try {

            var productDetail = await _dbContext.ProductDetails.FindAsync(id);
            if (productDetail == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.ProductDetails.Remove(productDetail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}