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
public class RollNumberController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public RollNumberController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allRollNumbers")]
    public async Task<IActionResult> GetRollNumbers() {
        try {

            var rollNumbers = await _dbContext.RollNumbers.Include(_ => _.ReceiptDetails).ToListAsync();

            return Ok(rollNumbers);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getRollNumber/{id}")]
    public async Task<IActionResult> GetRollNumberById(int id) {
        try {

            var rollNumber = await _dbContext.RollNumbers.Include(_ => _.ReceiptDetails).FirstOrDefaultAsync(_ => _.RollNumberId == id);

            if (rollNumber == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(rollNumber);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addRollNumber")]
    public async Task<IActionResult> AddRollNumber(RollNumberDto payloadRollNumber) {
        try {

            var receiptDetailExists = await _dbContext.Set<ReceiptDetail>().AnyAsync(_ => _.ReceiptDetailId == payloadRollNumber.ReceiptDetailId);
            if (!receiptDetailExists)
            {
                return BadRequest("Invalid ReceiptDetailId");
            }

            var newRollNumber = _mapper.Map<RollNumberDto, RollNumber>(payloadRollNumber);

            await _dbContext.RollNumbers.AddAsync(newRollNumber);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRollNumberById), new { id = newRollNumber.RollNumberId }, newRollNumber);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateRollNumber/{id}")]
    public async Task<IActionResult> UpdateRollNumber(int id, UpdateRollNumberDto payloadRollNumber) {
        try {

            var existingRollNumber = await _dbContext.RollNumbers.FindAsync(id);
            if (existingRollNumber == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadRollNumber.ReceiptDetailId.HasValue) {
                var receiptDetailExists = await _dbContext.Set<ReceiptDetail>().AnyAsync(_ => _.ReceiptDetailId == payloadRollNumber.ReceiptDetailId);
                if (!receiptDetailExists)
                {
                    return BadRequest("Invalid ReceiptDetailId");
                }
            }

            _mapper.Map(payloadRollNumber, existingRollNumber);

            _dbContext.RollNumbers.Update(existingRollNumber);
            await _dbContext.SaveChangesAsync();

            return Ok(existingRollNumber);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteRollNumber/{id}")]
    public async Task<IActionResult> DeleteRollNumber(int id) {
        try {

            var rollNumber = await _dbContext.RollNumbers.FindAsync(id);
            if (rollNumber == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.RollNumbers.Remove(rollNumber);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}