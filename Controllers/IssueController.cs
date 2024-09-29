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
public class IssueController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public IssueController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allIssues")]
    public async Task<IActionResult> GetIssues() {
        try {

            var issues = await _dbContext.Issues.Include(_ => _.RollNumbers).Include(_ => _.ProductStocks).ToListAsync();

            return Ok(issues);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getIssue/{id}")]
    public async Task<IActionResult> GetIssueById(int id) {
        try {

            var issue = await _dbContext.Issues.Include(_ => _.RollNumbers).Include(_ => _.ProductStocks).FirstOrDefaultAsync(_ => _.IssueId == id);

            if (issue == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(issue);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addIssue")]
    public async Task<IActionResult> AddIssue(IssueDto payloadIssue) {
        try {

            if (payloadIssue.RollNumberId.HasValue) {
                var rollNumberExists = await _dbContext.Set<RollNumber>().AnyAsync(_ => _.RollNumberId == payloadIssue.RollNumberId);
                if (!rollNumberExists)
                {
                    return BadRequest("Invalid RollNumberId");
                }
            }

            var productStockExists = await _dbContext.Set<ProductStock>().AnyAsync(_ => _.ProductStockId == payloadIssue.ProductStockId);
            if (!productStockExists)
            {
                return BadRequest("Invalid ProductStockId");
            }

            var newIssue = _mapper.Map<IssueDto, Issue>(payloadIssue);

            await _dbContext.Issues.AddAsync(newIssue);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIssueById), new { id = newIssue.IssueId }, newIssue);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateIssue/{id}")]
    public async Task<IActionResult> UpdateIssue(int id, UpdateIssueDto payloadIssue) {
        try {

            var existingIssue = await _dbContext.Issues.FindAsync(id);
            if (existingIssue == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadIssue.RollNumberId.HasValue) {
                var rollNumberExists = await _dbContext.Set<RollNumber>().AnyAsync(_ => _.RollNumberId == payloadIssue.RollNumberId);
                if (!rollNumberExists)
                {
                    return BadRequest("Invalid RollNumberId");
                }
            }

            if (payloadIssue.ProductStockId.HasValue) {
                var productStockExists = await _dbContext.Set<ProductStock>().AnyAsync(_ => _.ProductStockId == payloadIssue.ProductStockId);
                if (!productStockExists)
                {
                    return BadRequest("Invalid ProductStockId");
                }
            }
            
            _mapper.Map(payloadIssue, existingIssue);

            _dbContext.Issues.Update(existingIssue);
            await _dbContext.SaveChangesAsync();

            return Ok(existingIssue);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteIssue/{id}")]
    public async Task<IActionResult> DeleteIssue(int id) {
        try {

            var issue = await _dbContext.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.Issues.Remove(issue);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}