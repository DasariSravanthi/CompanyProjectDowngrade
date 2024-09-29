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
public class ProductionCalendaringController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ProductionCalendaringController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionCalendarings")]
    public async Task<IActionResult> GetProductionCalendarings() {
        try {

            var productionCalendarings = await _dbContext.ProductionCalendarings.Include(_ => _.ProductionCoatings).ToListAsync();

            return Ok(productionCalendarings);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getProductionCalendaring/{id}")]
    public async Task<IActionResult> GetProductionCalendaringById(int id) {
        try {

            var productionCalendaring = await _dbContext.ProductionCalendarings.Include(_ => _.ProductionCoatings).FirstOrDefaultAsync(_ => _.ProductionCalendaringId == id);

            if (productionCalendaring == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(productionCalendaring);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addProductionCalendaring")]
    public async Task<IActionResult> AddProductionCalendaring(ProductionCalendaringDto payloadProductionCalendaring) {
        try {

            var productionCoatingExists = await _dbContext.Set<ProductionCoating>().AnyAsync(_ => _.ProductionCoatingId == payloadProductionCalendaring.ProductionCoatingId);
            if (!productionCoatingExists)
            {
                return BadRequest("Invalid ProductionCoatingId");
            }

            var newProductionCalendaring = _mapper.Map<ProductionCalendaringDto, ProductionCalendaring>(payloadProductionCalendaring);

            await _dbContext.ProductionCalendarings.AddAsync(newProductionCalendaring);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductionCalendaringById), new { id = newProductionCalendaring.ProductionCalendaringId }, newProductionCalendaring);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateProductionCalendaring/{id}")]
    public async Task<IActionResult> UpdateProductionCalendaring(int id, UpdateProductionCalendaringDto payloadProductionCalendaring) {
        try {

            var existingProductionCalendaring = await _dbContext.ProductionCalendarings.FindAsync(id);
            if (existingProductionCalendaring == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadProductionCalendaring.ProductionCoatingId.HasValue) {
                var productionCoatingExists = await _dbContext.Set<ProductionCoating>().AnyAsync(_ => _.ProductionCoatingId == payloadProductionCalendaring.ProductionCoatingId);
                if (!productionCoatingExists)
                {
                    return BadRequest("Invalid ProductionCoatingId");
                }
            }

            _mapper.Map(payloadProductionCalendaring, existingProductionCalendaring);

            _dbContext.ProductionCalendarings.Update(existingProductionCalendaring);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProductionCalendaring);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteProductionCalendaring/{id}")]
    public async Task<IActionResult> DeleteProductionCalendaring(int id) {
        try {

            var productionCalendaring = await _dbContext.ProductionCalendarings.FindAsync(id);
            if (productionCalendaring == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.ProductionCalendarings.Remove(productionCalendaring);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}