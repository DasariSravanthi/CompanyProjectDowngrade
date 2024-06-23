using Microsoft.AspNetCore.Mvc;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;
using CompanyApp.Mapper.MapperService;

namespace CompanyApp.Controllers;

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
    public ActionResult<IEnumerable<Size>> GetSizes() {

        var sizes = _dbContext.Sizes.ToList();

        return Ok(sizes);
    }

    [HttpGet("getSize/{id}")]
    public ActionResult GetSizeById(byte id) {

        var size = _dbContext.Sizes.Find(id);

        if (size == null)
        {
            return NotFound();
        }

        return Ok(size);
    }

    [HttpPost("addSize")]
    public ActionResult AddSize(SizeDto payloadSize) {

        var newSize = _mapper.Map<SizeDto, Size>(payloadSize);

        _dbContext.Sizes.Add(newSize);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetSizeById), new { id = newSize.SizeId }, newSize);
    }

    [HttpPut("updateSize/{id}")]
    public ActionResult UpdateSize(byte id, UpdateSizeDto payloadSize) {

        var existingSize = _dbContext.Sizes.Find(id);
        if (existingSize == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadSize, existingSize);

        _dbContext.Sizes.Update(existingSize);
        _dbContext.SaveChanges();

        return Ok(existingSize);
    }

    [HttpDelete("deleteSize/{id}")]
    public ActionResult DeleteSize(byte id)
    {
        var size = _dbContext.Sizes.Find(id);
        if (size == null)
        {
            return NotFound();
        }

        _dbContext.Sizes.Remove(size);
        _dbContext.SaveChanges();

        return NoContent();
    }
}