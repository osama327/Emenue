using AutoMapper;
using Emenu.Core.Dto.Product;
using Emenu.Core.Interfaces;
using Emenu.Core.Model;
using Emenu.DAL;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Emenu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductController(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductCreateDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    return Ok("You can not insert becaus the Name is empty");
                }

                var isExists = await _unitOfWork.Products.FindSingleAsync(x => x.Name.Equals(request.Name));

                if (isExists != null)
                {
                    return Ok("You can not insert the product is already exists");
                }

                Product product = _mapper.Map<Product>(request);
                var res = await _unitOfWork.Products.AddAsync(product);
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
        [Route("UpdateProduct")]
        public async Task<ErrorOr<IActionResult>> UpdateProduct(ProductCreateDto request)
        {
            try
            {
                if (request == null)
                {
                    return Error.Failure(description: "the product is empty");
                }

                Product product = _mapper.Map<Product>(request);
                await _unitOfWork.Products.UpdateAsync(product);
                var res = await _unitOfWork.SaveAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]

        public async Task<ErrorOr<IActionResult>> DeleteProduct(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.FindAsync(id);
                if (product != null)
                {
                    await _unitOfWork.Products.DeleteAsync(product);
                }
                else
                {
                    return Error.Failure(description: "the product is not exists"); 
                }

                var res = await _unitOfWork.SaveAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllProduct")]
        public async Task<IActionResult> getAllProduct()
        {
            try
            {
                var res = await _unitOfWork.Products.FindListAsync(null,null,null);
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
        [Route("getProductById")]
        public async Task<IActionResult> getProductById(int id)
        {
            try
            {
                var res = await _unitOfWork.Products.FindAsync(id);
                if (res == null)
                {
                    return Ok("Not Found Data A Product");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getProductByName")]
        public async Task<IActionResult> getProductByName(string name)
        {
            try
            {
                var res = await _unitOfWork.Products.FindSingleAsync(x => x.Name.Equals(name));
                if (res == null)
                {
                    return Ok($"Not Found Data A Product {name}");
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
