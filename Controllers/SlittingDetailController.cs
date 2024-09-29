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
public class SlittingDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public SlittingDetailController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allSlittingDetails")]
    public async Task<IActionResult> GetSlittingDetails() {
        try {

            var slittingDetails = await _dbContext.SlittingDetails.Include(_ => _.ProductionSlittings).ToListAsync();

            return Ok(slittingDetails);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getSlittingDetail/{id}")]
    public async Task<IActionResult> GetSlittingDetailById(int id) {
        try {

            var slittingDetail = await _dbContext.SlittingDetails.Include(_ => _.ProductionSlittings).FirstOrDefaultAsync(_ => _.SlittingDetailId == id);

            if (slittingDetail == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(slittingDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addSlittingDetail")]
    public async Task<IActionResult> AddSlittingDetail(SlittingDetailDto payloadSlittingDetail) {
        try {

            var productionSlittingExists = await _dbContext.Set<ProductionSlitting>().AnyAsync(_ => _.ProductionSlittingId == payloadSlittingDetail.ProductionSlittingId);
            if (!productionSlittingExists)
            {
                return BadRequest("Invalid ProductionSlittingId");
            }

            var newSlittingDetail = _mapper.Map<SlittingDetailDto, SlittingDetail>(payloadSlittingDetail);

            await _dbContext.SlittingDetails.AddAsync(newSlittingDetail);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSlittingDetailById), new { id = newSlittingDetail.SlittingDetailId }, newSlittingDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateSlittingDetail/{id}")]
    public async Task<IActionResult> UpdateSlittingDetail(int id, UpdateSlittingDetailDto payloadSlittingDetail) {
        try {

            var existingSlittingDetail = await _dbContext.SlittingDetails.FindAsync(id);
            if (existingSlittingDetail == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadSlittingDetail.ProductionSlittingId.HasValue) {
                var productionSlittingExists = await _dbContext.Set<ProductionSlitting>().AnyAsync(_ => _.ProductionSlittingId == payloadSlittingDetail.ProductionSlittingId);
                if (!productionSlittingExists)
                {
                    return BadRequest("Invalid ProductionSlittingId");
                }
            }

            _mapper.Map(payloadSlittingDetail, existingSlittingDetail);

            _dbContext.SlittingDetails.Update(existingSlittingDetail);
            await _dbContext.SaveChangesAsync();

            return Ok(existingSlittingDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteSlittingDetail/{id}")]
    public async Task<IActionResult> DeleteSlittingDetail(int id) {
        try {

            var slittingDetail = await _dbContext.SlittingDetails.FindAsync(id);
            if (slittingDetail == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.SlittingDetails.Remove(slittingDetail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}