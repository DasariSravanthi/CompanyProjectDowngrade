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
public class SlittingDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public SlittingDetailController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allSlittingDetails")]
    public ActionResult<IEnumerable<SlittingDetail>> GetSlittingDetails() {

        var slittingDetails = _dbContext.SlittingDetails.Include(_ => _.ProductionSlittings).ToList();

        return Ok(slittingDetails);
    }

    [HttpGet("getSlittingDetail/{id}")]
    public ActionResult GetSlittingDetailById(int id) {

        var slittingDetail = _dbContext.SlittingDetails.Include(_ => _.ProductionSlittings).FirstOrDefault(_ => _.SlittingDetailId == id);

        if (slittingDetail == null)
        {
            return NotFound();
        }

        return Ok(slittingDetail);
    }

    [HttpPost("addSlittingDetail")]
    public ActionResult AddSlittingDetail(SlittingDetailDto payloadSlittingDetail) {

        var productionSlittingExists = _dbContext.Set<ProductionSlitting>().Any(_ => _.ProductionSlittingId == payloadSlittingDetail.ProductionSlittingId);
        if (!productionSlittingExists)
        {
            return BadRequest("Invalid ProductionSlittingId");
        }

        var newSlittingDetail = _mapper.Map<SlittingDetailDto, SlittingDetail>(payloadSlittingDetail);

        _dbContext.SlittingDetails.Add(newSlittingDetail);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetSlittingDetailById), new { id = newSlittingDetail.SlittingDetailId }, newSlittingDetail);
    }

    [HttpPut("updateSlittingDetail/{id}")]
    public ActionResult UpdateSlittingDetail(int id, UpdateSlittingDetailDto payloadSlittingDetail) {

        var existingSlittingDetail = _dbContext.SlittingDetails.Find(id);
        if (existingSlittingDetail == null)
        {
            return NotFound();
        }

        if (payloadSlittingDetail.ProductionSlittingId.HasValue) {
            var productionSlittingExists = _dbContext.Set<ProductionSlitting>().Any(_ => _.ProductionSlittingId == payloadSlittingDetail.ProductionSlittingId);
            if (!productionSlittingExists)
            {
                return BadRequest("Invalid ProductionSlittingId");
            }
        }

        _mapper.Map(payloadSlittingDetail, existingSlittingDetail);

        _dbContext.SlittingDetails.Update(existingSlittingDetail);
        _dbContext.SaveChanges();

        return Ok(existingSlittingDetail);
    }

    [HttpDelete("deleteSlittingDetail/{id}")]
    public ActionResult DeleteSlittingDetail(int id)
    {
        var slittingDetail = _dbContext.SlittingDetails.Find(id);
        if (slittingDetail == null)
        {
            return NotFound();
        }

        _dbContext.SlittingDetails.Remove(slittingDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}