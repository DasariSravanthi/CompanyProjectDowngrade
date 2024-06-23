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
public class ProductionCoatingController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductionCoatingController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionCoatings")]
    public ActionResult<IEnumerable<ProductionCoating>> GetProductionCoatings() {

        var productionCoatings = _dbContext.ProductionCoatings.Include(_ => _.Issues).ToList();

        return Ok(productionCoatings);
    }

    [HttpGet("getProductionCoating/{id}")]
    public ActionResult GetProductionCoatingById(int id) {

        var productionCoating = _dbContext.ProductionCoatings.Include(_ => _.Issues).FirstOrDefault(_ => _.ProductionCoatingId == id);

        if (productionCoating == null)
        {
            return NotFound();
        }

        return Ok(productionCoating);
    }

    [HttpPost("addProductionCoating")]
    public ActionResult AddProductionCoating(ProductionCoatingDto payloadProductionCoating) {

        var issueExists = _dbContext.Set<Issue>().Any(_ => _.IssueId == payloadProductionCoating.IssueId);
        if (!issueExists)
        {
            return BadRequest("Invalid IssueId");
        }

        var newProductionCoating = _mapper.Map<ProductionCoatingDto, ProductionCoating>(payloadProductionCoating);

        _dbContext.ProductionCoatings.Add(newProductionCoating);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductionCoatingById), new { id = newProductionCoating.ProductionCoatingId }, newProductionCoating);
    }

    [HttpPut("updateProductionCoating/{id}")]
    public ActionResult UpdateProductionCoating(int id, UpdateProductionCoatingDto payloadProductionCoating) {

        var existingProductionCoating = _dbContext.ProductionCoatings.Find(id);
        if (existingProductionCoating == null)
        {
            return NotFound();
        }

        if (payloadProductionCoating.IssueId.HasValue) {
            var issueExists = _dbContext.Set<Issue>().Any(_ => _.IssueId == payloadProductionCoating.IssueId);
            if (!issueExists)
            {
                return BadRequest("Invalid IssueId");
            }
        }

        _mapper.Map(payloadProductionCoating, existingProductionCoating);

        _dbContext.ProductionCoatings.Update(existingProductionCoating);
        _dbContext.SaveChanges();

        return Ok(existingProductionCoating);
    }

    [HttpDelete("deleteProductionCoating/{id}")]
    public ActionResult DeleteProductionCoating(int id)
    {
        var productionCoating = _dbContext.ProductionCoatings.Find(id);
        if (productionCoating == null)
        {
            return NotFound();
        }

        _dbContext.ProductionCoatings.Remove(productionCoating);
        _dbContext.SaveChanges();

        return NoContent();
    }
}