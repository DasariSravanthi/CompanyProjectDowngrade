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
public class SupplierController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;

    private readonly AppMapper _mapper;

    public SupplierController(CompanyDbContext dbContext, AppMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("allSuppliers")]
    public async Task<ActionResult> GetSuppliers() {
        try {

            var suppliers = await _dbContext.Suppliers.ToListAsync();

            return Ok(suppliers);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }   

    [HttpGet("getSupplier/{id}")]
    public async Task<IActionResult> GetSupplierById(byte id) {
        try {

            var supplier = await _dbContext.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(supplier);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addSupplier")]
    public async Task<IActionResult> AddSupplier(SupplierDto payloadSupplier) {
        try {

            var newSupplier = _mapper.Map<SupplierDto, Supplier>(payloadSupplier);

            await _dbContext.Suppliers.AddAsync(newSupplier);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplierById), new { id = newSupplier.SupplierId }, newSupplier);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateSupplier/{id}")]
    public async Task<IActionResult> UpdateSupplier(byte id, UpdateSupplierDto payloadSupplier) {
        try {

            var existingSupplier = await _dbContext.Suppliers.FindAsync(id);
            if (existingSupplier == null)
            {
                return NotFound("Data Not Found");
            }
            
            _mapper.Map(payloadSupplier, existingSupplier);

            _dbContext.Suppliers.Update(existingSupplier);
            await _dbContext.SaveChangesAsync();

            return Ok(existingSupplier);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteSupplier/{id}")]
    public async Task<IActionResult> DeleteSupplier(byte id) {
        try {

            var supplier = await _dbContext.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.Suppliers.Remove(supplier);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}