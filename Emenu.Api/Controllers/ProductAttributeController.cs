using AutoMapper;
using Emenu.Core.Dto.Product;
using Emenu.Core.Dto.ProductAttribute;
using Emenu.Core.Dto.Variant;
using Emenu.Core.Interfaces;
using Emenu.Core.Model;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Emenu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductAttributeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("AddProductAttribute")]
        public async Task<IActionResult> AddProductAttribute([FromBody] ProductAttributeDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    return Ok("You can not insert becaus the Name is empty");
                }

                var isExists = await _unitOfWork.ProductAttributes.FindSingleAsync(x => x.Name.Equals(request.Name));

                if (isExists != null)
                {
                    return Ok("You can not insert the product Attribute is already exists");
                }

                ProductAttribute product = _mapper.Map<ProductAttribute>(request);
                var res = await _unitOfWork.ProductAttributes.AddAsync(product);
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
        [Route("UpdateProductAttribute")]
        public async Task<IActionResult> UpdateProductAttribute(ProductAttributeDto request)
        {
            try
            {
                if (request == null)
                {
                    return Ok("the product is empty Attribute");
                }

                ProductAttribute product = _mapper.Map<ProductAttribute>(request);
                await _unitOfWork.ProductAttributes.UpdateAsync(product);
                var res = await _unitOfWork.SaveAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProductAttribute")]

        public async Task<IActionResult> DeleteProductAttribute(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductAttributes.FindAsync(id);
                if (product != null)
                {
                    await _unitOfWork.ProductAttributes.DeleteAsync(product);
                }
                else
                {
                    return Ok("the product Attribute is not exists");
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
        [Route("getAllProductAttribute")]
        public async Task<IActionResult> getAllProductAttribute()
        {
            try
            {
                var res = await _unitOfWork.ProductAttributes.FindListAsync(null, null, null);
                if (res == null)
                {
                    return Ok("Not Found Data");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getProductAttributeById")]
        public async Task<IActionResult> getProductAttributeById(int id)
        {
            try
            {
                var res = await _unitOfWork.ProductAttributes.FindAsync(id);
                if (res == null)
                {
                    return Ok("Not Found Data A Product Attributes");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getProductAttributeByName")]
        public async Task<IActionResult> getProductAttributeByName(string name)
        {
            try
            {
                var res = await _unitOfWork.ProductAttributes.FindSingleAsync(x => x.Name.Equals(name));
                if (res == null)
                {
                    return Ok($"Not Found Data A Product Attribute {name}");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("test_topup")]
        public async Task<IActionResult> test_topup( string msisdn, string msg)
        {
            try
            {
                if (string.IsNullOrEmpty(msisdn) || string.IsNullOrEmpty(msg))
                {
                    return Ok("You can not insert becaus the Name is empty");
                }

                ProductAttributeDto request = new ProductAttributeDto
                {
                    Id = 0,
                    Name = msg
                };

                ProductAttribute Variant = _mapper.Map<ProductAttribute>(request);
                var res = await _unitOfWork.ProductAttributes.AddAsync(Variant);
                await _unitOfWork.SaveAsync();

                if (res.IsError)
                {
                    return Ok(res.Errors.FirstOrDefault().Description);
                }
                return Ok("200");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
