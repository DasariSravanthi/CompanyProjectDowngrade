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
public class RollNumberController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public RollNumberController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allRollNumbers")]
    public ActionResult<IEnumerable<RollNumber>> GetRollNumbers() {

        var rollNumbers = _dbContext.RollNumbers.Include(_ => _.ReceiptDetails).ToList();

        return Ok(rollNumbers);
    }

    [HttpGet("getRollNumber/{id}")]
    public ActionResult GetRollNumberById(int id) {

        var rollNumber = _dbContext.RollNumbers.Include(_ => _.ReceiptDetails).FirstOrDefault(_ => _.RollNumberId == id);

        if (rollNumber == null)
        {
            return NotFound();
        }

        return Ok(rollNumber);
    }

    [HttpPost("addRollNumber")]
    public ActionResult AddRollNumber(RollNumberDto payloadRollNumber) {

        var receiptDetailExists = _dbContext.Set<ReceiptDetail>().Any(_ => _.ReceiptDetailId == payloadRollNumber.ReceiptDetailId);
        if (!receiptDetailExists)
        {
            return BadRequest("Invalid ReceiptDetailId");
        }

        var newRollNumber = _mapper.Map<RollNumberDto, RollNumber>(payloadRollNumber);

        _dbContext.RollNumbers.Add(newRollNumber);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetRollNumberById), new { id = newRollNumber.RollNumberId }, newRollNumber);
    }

    [HttpPut("updateRollNumber/{id}")]
    public ActionResult UpdateRollNumber(int id, UpdateRollNumberDto payloadRollNumber) {

        var existingRollNumber = _dbContext.RollNumbers.Find(id);
        if (existingRollNumber == null)
        {
            return NotFound();
        }

        if (payloadRollNumber.ReceiptDetailId.HasValue) {
            var receiptDetailExists = _dbContext.Set<ReceiptDetail>().Any(_ => _.ReceiptDetailId == payloadRollNumber.ReceiptDetailId);
            if (!receiptDetailExists)
            {
                return BadRequest("Invalid ReceiptDetailId");
            }
        }

        _mapper.Map(payloadRollNumber, existingRollNumber);

        _dbContext.RollNumbers.Update(existingRollNumber);
        _dbContext.SaveChanges();

        return Ok(existingRollNumber);
    }

    [HttpDelete("deleteRollNumber/{id}")]
    public ActionResult DeleteRollNumber(int id)
    {
        var rollNumber = _dbContext.RollNumbers.Find(id);
        if (rollNumber == null)
        {
            return NotFound();
        }

        _dbContext.RollNumbers.Remove(rollNumber);
        _dbContext.SaveChanges();

        return NoContent();
    }
}