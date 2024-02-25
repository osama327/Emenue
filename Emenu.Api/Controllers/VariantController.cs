using AutoMapper;
using Emenu.Core.Dto.ProductAttribute;
using Emenu.Core.Dto.Variant;
using Emenu.Core.Interfaces;
using Emenu.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Emenu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VariantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("AddVariant")]
        public async Task<IActionResult> AddVariant([FromBody] VariantDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    return Ok("You can not insert becaus the Name is empty");
                }   
                if (request.ProductAttributeId == null || request.ProductAttributeId == 0)
                {
                    return Ok("You can not insert becaus the Product Attribute is empty");
                }

                var isExists = await _unitOfWork.ProductAttributes.FindSingleAsync(x => x.Name.Equals(request.Name));

                if (isExists != null)
                {
                    return Ok("You can not insert the Variant is already exists");
                }

                Variant Variant = _mapper.Map<Variant>(request);
                var res = await _unitOfWork.Variants.AddAsync(Variant);
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
        [Route("UpdateVariant")]
        public async Task<IActionResult> UpdateVariant(VariantDto request)
        {
            try
            {
                if (request == null)
                {
                    return Ok("the Variant is empty Attribute");
                }

                Variant Variant = _mapper.Map<Variant>(request);
                await _unitOfWork.Variants.UpdateAsync(Variant);
                var res = await _unitOfWork.SaveAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteVariant")]
        public async Task<IActionResult> DeleteVariant(int id)
        {
            try
            {
                var Variant = await _unitOfWork.Variants.FindAsync(id);
                if (Variant != null)
                {
                    await _unitOfWork.Variants.DeleteAsync(Variant);
                }
                else
                {
                    return Ok("the Variant is not exists");
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
        [Route("getAllVariant")]
        public async Task<IActionResult> getAllVariant()
        {
            try
            {
                var res = await _unitOfWork.Variants.FindListAsync(null, null, null);
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
        [Route("getVariantById")]
        public async Task<IActionResult> getVariantById(int id)
        {
            try
            {
                var res = await _unitOfWork.Variants.FindAsync(id);
                if (res == null)
                {
                    return Ok("Not Found Data A Variant");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getVariantByName")]
        public async Task<IActionResult> getVariantByName(string name)
        {
            try
            {
                var res = await _unitOfWork.Variants.FindSingleAsync(x => x.Name.Equals(name));
                if (res == null)
                {
                    return Ok($"Not Found Data A Variant {name}");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
