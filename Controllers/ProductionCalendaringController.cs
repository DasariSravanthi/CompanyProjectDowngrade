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
public class ProductionCalendaringController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductionCalendaringController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionCalendarings")]
    public ActionResult<IEnumerable<ProductionCalendaring>> GetProductionCalendarings() {

        var productionCalendarings = _dbContext.ProductionCalendarings.Include(_ => _.ProductionCoatings).ToList();

        return Ok(productionCalendarings);
    }

    [HttpGet("getProductionCalendaring/{id}")]
    public ActionResult GetProductionCalendaringById(int id) {

        var productionCalendaring = _dbContext.ProductionCalendarings.Include(_ => _.ProductionCoatings).FirstOrDefault(_ => _.ProductionCalendaringId == id);

        if (productionCalendaring == null)
        {
            return NotFound();
        }

        return Ok(productionCalendaring);
    }

    [HttpPost("addProductionCalendaring")]
    public ActionResult AddProductionCalendaring(ProductionCalendaringDto payloadProductionCalendaring) {

        var productionCoatingExists = _dbContext.Set<ProductionCoating>().Any(_ => _.ProductionCoatingId == payloadProductionCalendaring.ProductionCoatingId);
        if (!productionCoatingExists)
        {
            return BadRequest("Invalid ProductionCoatingId");
        }

        var newProductionCalendaring = _mapper.Map<ProductionCalendaringDto, ProductionCalendaring>(payloadProductionCalendaring);

        _dbContext.ProductionCalendarings.Add(newProductionCalendaring);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductionCalendaringById), new { id = newProductionCalendaring.ProductionCalendaringId }, newProductionCalendaring);
    }

    [HttpPut("updateProductionCalendaring/{id}")]
    public ActionResult UpdateProductionCalendaring(int id, UpdateProductionCalendaringDto payloadProductionCalendaring) {

        var existingProductionCalendaring = _dbContext.ProductionCalendarings.Find(id);
        if (existingProductionCalendaring == null)
        {
            return NotFound();
        }

        if (payloadProductionCalendaring.ProductionCoatingId.HasValue) {
            var productionCoatingExists = _dbContext.Set<ProductionCoating>().Any(_ => _.ProductionCoatingId == payloadProductionCalendaring.ProductionCoatingId);
            if (!productionCoatingExists)
            {
                return BadRequest("Invalid ProductionCoatingId");
            }
        }

        _mapper.Map(payloadProductionCalendaring, existingProductionCalendaring);

        _dbContext.ProductionCalendarings.Update(existingProductionCalendaring);
        _dbContext.SaveChanges();

        return Ok(existingProductionCalendaring);
    }

    [HttpDelete("deleteProductionCalendaring/{id}")]
    public ActionResult DeleteProductionCalendaring(int id)
    {
        var productionCalendaring = _dbContext.ProductionCalendarings.Find(id);
        if (productionCalendaring == null)
        {
            return NotFound();
        }

        _dbContext.ProductionCalendarings.Remove(productionCalendaring);
        _dbContext.SaveChanges();

        return NoContent();
    }
}