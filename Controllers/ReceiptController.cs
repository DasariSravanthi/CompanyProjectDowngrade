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
public class ReceiptController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ReceiptController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allReceipts")]
    public async Task<IActionResult> GetReceipts() {
        try {

            var receipts = await _dbContext.Receipts.Include(_ => _.Suppliers).ToListAsync();

            return Ok(receipts);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getReceipt/{id}")]
    public async Task<IActionResult> GetReceiptById(int id) {
        try {

            var receipt = await _dbContext.Receipts.Include(_ => _.Suppliers).FirstOrDefaultAsync(_ => _.ReceiptId == id);

            if (receipt == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(receipt);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addReceipt")]
    public async Task<IActionResult> AddReceipt(ReceiptDto payloadReceipt) {
        try {

            var supplierExists = await _dbContext.Set<Supplier>().AnyAsync(_ => _.SupplierId == payloadReceipt.SupplierId);
            if (!supplierExists)
            {
                return BadRequest("Invalid SupplierId");
            }

            var newReceipt = _mapper.Map<ReceiptDto, Receipt>(payloadReceipt);

            await _dbContext.Receipts.AddAsync(newReceipt);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReceiptById), new { id = newReceipt.ReceiptId }, newReceipt);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateReceipt/{id}")]
    public async Task<IActionResult> UpdateReceipt(int id, UpdateReceiptDto payloadReceipt) {
        try {

            var existingReceipt = await _dbContext.Receipts.FindAsync(id);
            if (existingReceipt == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadReceipt.SupplierId.HasValue) {
                var supplierExists = await _dbContext.Set<Supplier>().AnyAsync(_ => _.SupplierId == payloadReceipt.SupplierId);
                if (!supplierExists)
                {
                    return BadRequest("Invalid SupplierId");
                }
            }

            _mapper.Map(payloadReceipt, existingReceipt);

            _dbContext.Receipts.Update(existingReceipt);
            await _dbContext.SaveChangesAsync();

            return Ok(existingReceipt);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteReceipt/{id}")]
    public ActionResult DeleteReceipt(int id) {
        try {
            
            var receipt = _dbContext.Receipts.Find(id);
            if (receipt == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.Receipts.Remove(receipt);
            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}