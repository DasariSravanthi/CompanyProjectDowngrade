using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;
using CompanyApp.Mapper.MapperService;

namespace CompanyApp.Controllers;

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
    public ActionResult<IEnumerable<ProductDetail>> GetProductDetails() {

        var productDetails = _dbContext.ProductDetails.Include(_ => _.Products).ToList();

        return Ok(productDetails);
    }

    [HttpGet("getProductDetail/{id}")]
    public ActionResult GetProductDetailById(byte id) {

        var productDetail = _dbContext.ProductDetails.Include(_ => _.Products).FirstOrDefault(_ => _.ProductDetailId == id);

        if (productDetail == null)
        {
            return NotFound();
        }

        return Ok(productDetail);
    }

    [HttpPost("addProductDetail")]
    public ActionResult AddProductDetail(ProductDetailDto payloadProductDetail) {

        //Validate foreign key

        var productExists = _dbContext.Set<Product>().Any(_ => _.ProductId == payloadProductDetail.ProductId);
        if (!productExists)
        {
            return BadRequest("Invalid ProductId");
        }

        var newProductDetail = _mapper.Map<ProductDetailDto, ProductDetail>(payloadProductDetail);

        _dbContext.ProductDetails.Add(newProductDetail);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductDetailById), new { id = newProductDetail.ProductDetailId }, newProductDetail);
    }

    [HttpPut("updateProductDetail/{id}")]
    public ActionResult UpdateProductDetail(byte id, UpdateProductDetailDto payloadProductDetail) {

        var existingProductDetail = _dbContext.ProductDetails.Find(id);
        if (existingProductDetail == null)
        {
            return NotFound();
        }

        if (payloadProductDetail.ProductId.HasValue) {
            var productExists = _dbContext.Set<Product>().Any(_ => _.ProductId == payloadProductDetail.ProductId);
            if (!productExists)
            {
                return BadRequest("Invalid ProductId");
            }
        }

        _mapper.Map(payloadProductDetail, existingProductDetail);

        _dbContext.ProductDetails.Update(existingProductDetail);
        _dbContext.SaveChanges();

        return Ok(existingProductDetail);
    }

    [HttpDelete("deleteProductDetail/{id}")]
    public ActionResult DeleteProductDetail(byte id)
    {
        var productDetail = _dbContext.ProductDetails.Find(id);
        if (productDetail == null)
        {
            return NotFound();
        }

        _dbContext.ProductDetails.Remove(productDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}