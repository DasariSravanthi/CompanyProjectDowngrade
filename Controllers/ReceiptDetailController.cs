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
public class ReceiptDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;

    public ReceiptDetailController(CompanyDbContext dbContext,  AppMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allReceiptDetails")]
    public async Task<IActionResult> GetReceiptDetails() {
        try {

            var receiptDetails = await _dbContext.ReceiptDetails.Include(_ => _.Receipts).Include(_ => _.ProductStocks).ToListAsync();

            return Ok(receiptDetails);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getReceiptDetail/{id}")]
    public async Task<IActionResult> GetReceiptDetailById(int id) {
        try {

            var receiptDetail = await _dbContext.ReceiptDetails.Include(_ => _.Receipts).Include(_ => _.ProductStocks).FirstOrDefaultAsync(_ => _.ReceiptDetailId == id);

            if (receiptDetail == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(receiptDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addReceiptDetail")]
    public async Task<IActionResult> AddReceiptDetail(ReceiptDetailDto payloadReceiptDetail) {
        try {

            var receiptExists = await _dbContext.Set<Receipt>().AnyAsync(_ => _.ReceiptId == payloadReceiptDetail.ReceiptId);
            if (!receiptExists)
            {
                return BadRequest("Invalid ReceiptId");
            }

            var productStockExists = await _dbContext.Set<ProductStock>().AnyAsync(_ => _.ProductStockId == payloadReceiptDetail.ProductStockId);
            if (!productStockExists)
            {
                return BadRequest("Invalid ProductStockId");
            }
            
            var newReceiptDetail = _mapper.Map<ReceiptDetailDto, ReceiptDetail>(payloadReceiptDetail);

            await _dbContext.ReceiptDetails.AddAsync(newReceiptDetail);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReceiptDetailById), new { id = newReceiptDetail.ReceiptDetailId }, newReceiptDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateReceiptDetail/{id}")]
    public async Task<IActionResult> UpdateReceiptDetail(int id, UpdateReceiptDetailDto payloadReceiptDetail) {
        try {

            var existingReceiptDetail = await _dbContext.ReceiptDetails.FindAsync(id);
            if (existingReceiptDetail == null)
            {
                return NotFound("Data Not Found");
            }

            if (payloadReceiptDetail.ReceiptId.HasValue) {
                var receiptExists = await _dbContext.Set<Receipt>().AnyAsync(_ => _.ReceiptId == payloadReceiptDetail.ReceiptId);
                if (!receiptExists)
                {
                    return BadRequest("Invalid ReceiptId");
                }
            }

            if (payloadReceiptDetail.ProductStockId.HasValue) {
                var productStockExists = await _dbContext.Set<ProductStock>().AnyAsync(_ => _.ProductStockId == payloadReceiptDetail.ProductStockId);
                if (!productStockExists)
                {
                    return BadRequest("Invalid ProductStockId");
                }
            }

            _mapper.Map(payloadReceiptDetail, existingReceiptDetail);

            _dbContext.ReceiptDetails.Update(existingReceiptDetail);
            await _dbContext.SaveChangesAsync();

            return Ok(existingReceiptDetail);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = IdentityConstants.PolicyName1)]
    [HttpDelete("deleteReceiptDetail/{id}")]
    public async Task<IActionResult> DeleteReceiptDetail(int id) {
        try {

            var receiptDetail = await _dbContext.ReceiptDetails.FindAsync(id);
            if (receiptDetail == null)
            {
                return NotFound("Data Not Found");
            }

            _dbContext.ReceiptDetails.Remove(receiptDetail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
}