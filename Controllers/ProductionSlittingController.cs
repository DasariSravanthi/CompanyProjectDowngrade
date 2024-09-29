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
public class ProductionSlittingController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductionSlittingController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionSlittings")]
    public async Task<IActionResult> GetProductionSlittings() {
        try {

            var productionSlittings = await _dbContext.ProductionSlittings.Include(_ => _.ProductionCalendarings).ToListAsync();

            return Ok(productionSlittings);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getProductionSlitting/{id}")]
    public async Task<IActionResult> GetProductionSlittingById(int id) {
        try {

            var productionSlitting = await _dbContext.ProductionSlittings.Include(_ => _.ProductionCalendarings).FirstOrDefaultAsync(_ => _.ProductionSlittingId == id);

            if (productionSlitting == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(productionSlitting);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProductionSlitting")]
    public async Task<IActionResult> AddProductionSlitting(ProductionSlittingDto payloadProductionSlitting) {
        try {

            var productionCalendaringExists = await _dbContext.Set<ProductionCalendaring>().AnyAsync(_ => _.ProductionCalendaringId == payloadProductionSlitting.ProductionCalendaringId);
            if (!productionCalendaringExists)
            {
                return BadRequest("Invalid ProductionCalendaringId");
            }

            var newProductionSlitting = _mapper.Map<ProductionSlittingDto, ProductionSlitting>(payloadProductionSlitting);

            await _dbContext.ProductionSlittings.AddAsync(newProductionSlitting);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductionSlittingById), new { id = newProductionSlitting.ProductionSlittingId }, newProductionSlitting);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateProductionSlitting/{id}")]
    public async Task<IActionResult> UpdateProductionSlitting(int id, UpdateProductionSlittingDto payloadProductionSlitting) {
        try {

            var existingProductionSlitting = await _dbContext.ProductionSlittings.FindAsync(id);
            if (existingProductionSlitting == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadProductionSlitting.ProductionCalendaringId.HasValue) {
                var productionCalendaringExists = await _dbContext.Set<ProductionCalendaring>().AnyAsync(_ => _.ProductionCalendaringId == payloadProductionSlitting.ProductionCalendaringId);
                if (!productionCalendaringExists)
                {
                    return BadRequest("Invalid ProductionCalendaringId");
                }
            }

            _mapper.Map(payloadProductionSlitting, existingProductionSlitting);

            _dbContext.ProductionSlittings.Update(existingProductionSlitting);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProductionSlitting);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteProductionSlitting/{id}")]
    public async Task<IActionResult> DeleteProductionSlitting(int id) {
        try {

            var productionSlitting = await _dbContext.ProductionSlittings.FindAsync(id);
            if (productionSlitting == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.ProductionSlittings.Remove(productionSlitting);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}