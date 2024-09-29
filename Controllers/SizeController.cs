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
public class SizeController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public SizeController(CompanyDbContext dbContext, AppMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("allSizes")]
    public async Task<IActionResult> GetSizes() {
        try {

            var sizes = await _dbContext.Sizes.ToListAsync();

            return Ok(sizes);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getSize/{id}")]
    public async Task<IActionResult> GetSizeById(byte id) {
        try {

            var size = await _dbContext.Sizes.FindAsync(id);

            if (size == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(size);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addSize")]
    public async Task<IActionResult> AddSize(SizeDto payloadSize) {
        try {

            var newSize = _mapper.Map<SizeDto, Size>(payloadSize);

            await _dbContext.Sizes.AddAsync(newSize);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSizeById), new { id = newSize.SizeId }, newSize);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateSize/{id}")]
    public async Task<IActionResult> UpdateSize(byte id, UpdateSizeDto payloadSize) {
        try {

            var existingSize = await _dbContext.Sizes.FindAsync(id);
            if (existingSize == null)
            {
                return NotFound("Data Not Found");
            }

            _mapper.Map(payloadSize, existingSize);

            _dbContext.Sizes.Update(existingSize);
            await _dbContext.SaveChangesAsync();

            return Ok(existingSize);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteSize/{id}")]
    public async Task<IActionResult> DeleteSize(byte id) {
        try {

            var size = await _dbContext.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.Sizes.Remove(size);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}