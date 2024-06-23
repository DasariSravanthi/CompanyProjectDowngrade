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
public class IssueController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public IssueController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allIssues")]
    public ActionResult<IEnumerable<Issue>> GetIssues() {

        var issues = _dbContext.Issues.Include(_ => _.RollNumbers).Include(_ => _.ProductStocks).ToList();

        return Ok(issues);
    }

    [HttpGet("getIssue/{id}")]
    public ActionResult GetIssueById(int id) {

        var issue = _dbContext.Issues.Include(_ => _.RollNumbers).Include(_ => _.ProductStocks).FirstOrDefault(_ => _.IssueId == id);

        if (issue == null)
        {
            return NotFound();
        }

        return Ok(issue);
    }

    [HttpPost("addIssue")]
    public ActionResult AddIssue(IssueDto payloadIssue) {

        if (payloadIssue.RollNumberId.HasValue) {
            var rollNumberExists = _dbContext.Set<RollNumber>().Any(_ => _.RollNumberId == payloadIssue.RollNumberId);
            if (!rollNumberExists)
            {
                return BadRequest("Invalid RollNumberId");
            }
        }

        var productStockExists = _dbContext.Set<ProductStock>().Any(_ => _.ProductStockId == payloadIssue.ProductStockId);
        if (!productStockExists)
        {
            return BadRequest("Invalid ProductStockId");
        }

        var newIssue = _mapper.Map<IssueDto, Issue>(payloadIssue);

        _dbContext.Issues.Add(newIssue);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetIssueById), new { id = newIssue.IssueId }, newIssue);
    }

    [HttpPut("updateIssue/{id}")]
    public ActionResult UpdateIssue(int id, UpdateIssueDto payloadIssue) {

        var existingIssue = _dbContext.Issues.Find(id);
        if (existingIssue == null)
        {
            return NotFound();
        }

        if (payloadIssue.RollNumberId.HasValue) {
            var rollNumberExists = _dbContext.Set<RollNumber>().Any(_ => _.RollNumberId == payloadIssue.RollNumberId);
            if (!rollNumberExists)
            {
                return BadRequest("Invalid RollNumberId");
            }
        }

        if (payloadIssue.ProductStockId.HasValue) {
            var productStockExists = _dbContext.Set<ProductStock>().Any(_ => _.ProductStockId == payloadIssue.ProductStockId);
            if (!productStockExists)
            {
                return BadRequest("Invalid ProductStockId");
            }
        }
        
        _mapper.Map(payloadIssue, existingIssue);

        _dbContext.Issues.Update(existingIssue);
        _dbContext.SaveChanges();

        return Ok(existingIssue);
    }

    [HttpDelete("deleteIssue/{id}")]
    public ActionResult DeleteIssue(int id)
    {
        var issue = _dbContext.Issues.Find(id);
        if (issue == null)
        {
            return NotFound();
        }

        _dbContext.Issues.Remove(issue);
        _dbContext.SaveChanges();

        return NoContent();
    }
}