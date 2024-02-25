using AutoMapper;
using Emenu.Core.Dto.ProductAttribute;
using Emenu.Core.Dto.Stock;
using Emenu.Core.Dto.Variant;
using Emenu.Core.Interfaces;
using Emenu.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Emenu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddProductToStock")]
        public async Task<IActionResult> AddProductToStock([FromBody] StockDto request)
        {
            try
            {
                if (request.ProductId == null || request.ProductId == 0)
                {
                    return Ok("You can not insert becaus no product selected");
                }


                Stock Stock = _mapper.Map<Stock>(request);
                var res = await _unitOfWork.Stocks.AddAsync(Stock);
                await _unitOfWork.SaveAsync();

                if (res.IsError)
                {
                    return Ok(res.Errors.FirstOrDefault().Description);
                }
                return Ok(res.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateProductStock")]
        public async Task<IActionResult> UpdateProductStock(StockDto request)
        {
            try
            {
                if (request == null)
                {
                    return Ok("the Stock is empty ");
                }

                Stock Stock = _mapper.Map<Stock>(request);
                await _unitOfWork.Stocks.UpdateAsync(Stock);
                var res = await _unitOfWork.SaveAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProductStock")]
        public async Task<IActionResult> DeleteProductStock(int id)
        {
            try
            {
                var Stock = await _unitOfWork.Stocks.FindAsync(id);
                if (Stock != null)
                {
                    await _unitOfWork.Stocks.DeleteAsync(Stock);
                }
                else
                {
                    return Ok("the product is not exists");
                }

                var res = await _unitOfWork.SaveAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllProductsGrouping")]
        public async Task<IActionResult> getAllProductsGrouping()
        {
            try
            {
                var res =  await _unitOfWork.Stocks.AllProductsGrouping();
                if (res.IsError)
                {
                    return Ok(res.Errors.FirstOrDefault().Description);
                }

                return Ok(res.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getProductsStock")]
        public async Task<IActionResult> getProductsStock(int id)
        {
            try
            {
                var res = await _unitOfWork.Stocks.ProductGrouping(id);
                if (res.IsError)
                {
                    return Ok(res.Errors.FirstOrDefault().Description);
                }

                return Ok(res.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
