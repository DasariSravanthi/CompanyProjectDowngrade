using Microsoft.AspNetCore.Mvc;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;
using CompanyApp.Mapper.MapperService;

namespace CompanyApp.Controllers;

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
    public ActionResult<IEnumerable<Supplier>> GetSuppliers() {

        var suppliers = _dbContext.Suppliers.ToList();

        return Ok(suppliers);
    }   

    [HttpGet("getSupplier/{id}")]
    public ActionResult GetSupplierById(byte id) {

        var supplier = _dbContext.Suppliers.Find(id);

        if (supplier == null)
        {
            return NotFound();
        }

        return Ok(supplier);
    }

    [HttpPost("addSupplier")]
    public ActionResult AddSupplier(SupplierDto payloadSupplier) {

        var newSupplier = _mapper.Map<SupplierDto, Supplier>(payloadSupplier);

        _dbContext.Suppliers.Add(newSupplier);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetSupplierById), new { id = newSupplier.SupplierId }, newSupplier);
    }

    [HttpPut("updateSupplier/{id}")]
    public ActionResult UpdateSupplier(byte id, UpdateSupplierDto payloadSupplier) {

        var existingSupplier = _dbContext.Suppliers.Find(id);
        if (existingSupplier == null)
        {
            return NotFound();
        }
        
        _mapper.Map(payloadSupplier, existingSupplier);

        _dbContext.Suppliers.Update(existingSupplier);
        _dbContext.SaveChanges();

        return Ok(existingSupplier);
    }

    [HttpDelete("deleteSupplier/{id}")]
    public ActionResult DeleteSupplier(byte id)
    {
        var supplier = _dbContext.Suppliers.Find(id);
        if (supplier == null)
        {
            return NotFound();
        }

        _dbContext.Suppliers.Remove(supplier);
        _dbContext.SaveChanges();

        return NoContent();
    }
}