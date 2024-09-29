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
public class ProductionCoatingController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductionCoatingController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionCoatings")]
    public async Task<IActionResult> GetProductionCoatings() {
        try {

            var productionCoatings = await _dbContext.ProductionCoatings.Include(_ => _.Issues).ToListAsync();

            return Ok(productionCoatings);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getProductionCoating/{id}")]
    public async Task<IActionResult> GetProductionCoatingById(int id) {
        try {

            var productionCoating = await _dbContext.ProductionCoatings.Include(_ => _.Issues).FirstOrDefaultAsync(_ => _.ProductionCoatingId == id);

            if (productionCoating == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(productionCoating);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProductionCoating")]
    public async Task<IActionResult> AddProductionCoating(ProductionCoatingDto payloadProductionCoating) {
        try {

            var issueExists = await _dbContext.Set<Issue>().AnyAsync(_ => _.IssueId == payloadProductionCoating.IssueId);
            if (!issueExists)
            {
                return BadRequest("Invalid IssueId");
            }

            var newProductionCoating = _mapper.Map<ProductionCoatingDto, ProductionCoating>(payloadProductionCoating);

            await _dbContext.ProductionCoatings.AddAsync(newProductionCoating);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductionCoatingById), new { id = newProductionCoating.ProductionCoatingId }, newProductionCoating);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateProductionCoating/{id}")]
    public async Task<IActionResult> UpdateProductionCoating(int id, UpdateProductionCoatingDto payloadProductionCoating) {
        try {

            var existingProductionCoating = await _dbContext.ProductionCoatings.FindAsync(id);
            if (existingProductionCoating == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadProductionCoating.IssueId.HasValue) {
                var issueExists = await _dbContext.Set<Issue>().AnyAsync(_ => _.IssueId == payloadProductionCoating.IssueId);
                if (!issueExists)
                {
                    return BadRequest("Invalid IssueId");
                }
            }

            _mapper.Map(payloadProductionCoating, existingProductionCoating);

            _dbContext.ProductionCoatings.Update(existingProductionCoating);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProductionCoating);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteProductionCoating/{id}")]
    public async Task<IActionResult> DeleteProductionCoating(int id) {
        try {

            var productionCoating = await _dbContext.ProductionCoatings.FindAsync(id);
            if (productionCoating == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.ProductionCoatings.Remove(productionCoating);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}