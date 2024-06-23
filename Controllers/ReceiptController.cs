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
public class ReceiptController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ReceiptController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allReceipts")]
    public ActionResult<IEnumerable<Receipt>> GetReceipts() {

        var receipts = _dbContext.Receipts.Include(_ => _.Suppliers).ToList();

        return Ok(receipts);
    }

    [HttpGet("getReceipt/{id}")]
    public ActionResult GetReceiptById(int id) {

        var receipt = _dbContext.Receipts.Include(_ => _.Suppliers).FirstOrDefault(_ => _.ReceiptId == id);

        if (receipt == null)
        {
            return NotFound();
        }

        return Ok(receipt);
    }

    [HttpPost("addReceipt")]
    public ActionResult AddReceipt(ReceiptDto payloadReceipt) {

        var supplierExists = _dbContext.Set<Supplier>().Any(_ => _.SupplierId == payloadReceipt.SupplierId);
        if (!supplierExists)
        {
            return BadRequest("Invalid SupplierId");
        }

        var newReceipt = _mapper.Map<ReceiptDto, Receipt>(payloadReceipt);

        _dbContext.Receipts.Add(newReceipt);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetReceiptById), new { id = newReceipt.ReceiptId }, newReceipt);
    }

    [HttpPut("updateReceipt/{id}")]
    public ActionResult UpdateReceipt(int id, UpdateReceiptDto payloadReceipt) {

        var existingReceipt = _dbContext.Receipts.Find(id);
        if (existingReceipt == null)
        {
            return NotFound();
        }

        if (payloadReceipt.SupplierId.HasValue) {
            var supplierExists = _dbContext.Set<Supplier>().Any(_ => _.SupplierId == payloadReceipt.SupplierId);
            if (!supplierExists)
            {
                return BadRequest("Invalid SupplierId");
            }
        }

        _mapper.Map(payloadReceipt, existingReceipt);

        _dbContext.Receipts.Update(existingReceipt);
        _dbContext.SaveChanges();

        return Ok(existingReceipt);
    }

    [HttpDelete("deleteReceipt/{id}")]
    public ActionResult DeleteReceipt(int id)
    {
        var receipt = _dbContext.Receipts.Find(id);
        if (receipt == null)
        {
            return NotFound();
        }

        _dbContext.Receipts.Remove(receipt);
        _dbContext.SaveChanges();

        return NoContent();
    }
}