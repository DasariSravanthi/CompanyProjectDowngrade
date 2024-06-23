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
public class ReceiptDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ReceiptDetailController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allReceiptDetails")]
    public ActionResult<IEnumerable<ReceiptDetail>> GetReceiptDetails() {

        var receiptDetails = _dbContext.ReceiptDetails.Include(_ => _.Receipts).Include(_ => _.ProductStocks).ToList();

        return Ok(receiptDetails);
    }

    [HttpGet("getReceiptDetail/{id}")]
    public ActionResult GetReceiptDetailById(int id) {

        var receiptDetail = _dbContext.ReceiptDetails.Include(_ => _.Receipts).Include(_ => _.ProductStocks).FirstOrDefault(_ => _.ReceiptDetailId == id);

        if (receiptDetail == null)
        {
            return NotFound();
        }

        return Ok(receiptDetail);
    }

    [HttpPost("addReceiptDetail")]
    public ActionResult AddReceiptDetail(ReceiptDetailDto payloadReceiptDetail) {

        var receiptExists = _dbContext.Set<Receipt>().Any(_ => _.ReceiptId == payloadReceiptDetail.ReceiptId);
        if (!receiptExists)
        {
            return BadRequest("Invalid ReceiptId");
        }

        var productStockExists = _dbContext.Set<ProductStock>().Any(_ => _.ProductStockId == payloadReceiptDetail.ProductStockId);
        if (!productStockExists)
        {
            return BadRequest("Invalid ProductStockId");
        }
        
        var newReceiptDetail = _mapper.Map<ReceiptDetailDto, ReceiptDetail>(payloadReceiptDetail);

        _dbContext.ReceiptDetails.Add(newReceiptDetail);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetReceiptDetailById), new { id = newReceiptDetail.ReceiptDetailId }, newReceiptDetail);
    }

    [HttpPut("updateReceiptDetail/{id}")]
    public ActionResult UpdateReceiptDetail(int id, UpdateReceiptDetailDto payloadReceiptDetail) {

        var existingReceiptDetail = _dbContext.ReceiptDetails.Find(id);
        if (existingReceiptDetail == null)
        {
            return NotFound();
        }

        if (payloadReceiptDetail.ReceiptId.HasValue) {
            var receiptExists = _dbContext.Set<Receipt>().Any(_ => _.ReceiptId == payloadReceiptDetail.ReceiptId);
            if (!receiptExists)
            {
                return BadRequest("Invalid ReceiptId");
            }
        }

        if (payloadReceiptDetail.ProductStockId.HasValue) {
            var productStockExists = _dbContext.Set<ProductStock>().Any(_ => _.ProductStockId == payloadReceiptDetail.ProductStockId);
            if (!productStockExists)
            {
                return BadRequest("Invalid ProductStockId");
            }
        }

        _mapper.Map(payloadReceiptDetail, existingReceiptDetail);

        _dbContext.ReceiptDetails.Update(existingReceiptDetail);
        _dbContext.SaveChanges();

        return Ok(existingReceiptDetail);
    }

    [HttpDelete("deleteReceiptDetail/{id}")]
    public ActionResult DeleteReceiptDetail(int id)
    {
        var receiptDetail = _dbContext.ReceiptDetails.Find(id);
        if (receiptDetail == null)
        {
            return NotFound();
        }

        _dbContext.ReceiptDetails.Remove(receiptDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}