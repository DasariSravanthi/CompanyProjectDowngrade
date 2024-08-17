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
public class ProductStockController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductStockController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductStocks")]
    public ActionResult<IEnumerable<ProductStock>> GetProductStocks() {

        var productStocks = _dbContext.ProductStocks.Include(_ => _.ProductDetails).Include(_ => _.Sizes).ToList();

        return Ok(productStocks);
    }

    [HttpGet("getProductStock/{id}")]
    public ActionResult GetProductStockById(Int16 id) {

        var productStock = _dbContext.ProductStocks.Include(_ => _.ProductDetails).Include(_ => _.Sizes).FirstOrDefault(_ => _.ProductStockId == id);

        if (productStock == null)
        {
            return NotFound();
        }

        return Ok(productStock);
    }

    [HttpPost("addProductStock")]
    public ActionResult AddProductStock(ProductStockDto payloadProductStock) {

        var productDetailExists = _dbContext.Set<ProductDetail>().Any(_ => _.ProductDetailId == payloadProductStock.ProductDetailId);
        if (!productDetailExists)
        {
            return BadRequest("Invalid ProductDetailId");
        }

        if (payloadProductStock.SizeId.HasValue) {
            var sizeExists = _dbContext.Set<Size>().Any(_ => _.SizeId == payloadProductStock.SizeId);
            if (!sizeExists)
            {
                return BadRequest("Invalid SizeId");
            }
        }

        var newProductStock = _mapper.Map<ProductStockDto, ProductStock>(payloadProductStock);

        _dbContext.ProductStocks.Add(newProductStock);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductStockById), new { id = newProductStock.ProductStockId }, newProductStock);
    }

    [HttpPut("updateProductStock/{id}")]
    public ActionResult UpdateProductStock(Int16 id, UpdateProductStockDto payloadProductStock) {

        var existingProductStock = _dbContext.ProductStocks.Find(id);
        if (existingProductStock == null)
        {
            return NotFound();
        }

        if (payloadProductStock.ProductDetailId.HasValue) {
            var productDetailExists = _dbContext.Set<ProductDetail>().Any(_ => _.ProductDetailId == payloadProductStock.ProductDetailId);
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
        _dbContext.SaveChanges();

        return Ok(existingProductStock);
    }

    [HttpDelete("deleteProductStock/{id}")]
    public ActionResult DeleteProductStock(Int16 id)
    {
        var productDetail = _dbContext.ProductStocks.Find(id);
        if (productDetail == null)
        {
            return NotFound();
        }

        _dbContext.ProductStocks.Remove(productDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}