using AutoMapper;
using Emenu.Core.Dto.Image;
using Emenu.Core.Dto.Variant;
using Emenu.Core.Interfaces;
using Emenu.Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Emenu.Core.Model;
using Emenu.Core.Dto.Product;

namespace Emenu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public ImageController(IUnitOfWork unitOfWork, IMapper mapper , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSetting.ImagePath}";
        }



        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> AddImage([FromForm] ImageDto request)
        {
            try
            {
                if (request.productId == null || request.productId == 0)
                {
                    return Ok("You can not insert becaus the product is empty");
                }
                if (request.Photo == null)
                {
                    return Ok("You can not insert becaus the Photo is empty");
                }

                if (request.IsBasec == true)
                {
                    var count = await _unitOfWork.Images.FindListAsync(x => x.productId == request.productId, null, null);

                    if (count.Count != 0)
                    {
                        foreach (var item in count)
                        {
                            if (item.IsBasec == true)
                            {
                                var res_Images = await _unitOfWork.Images.FindAsync(item.Id);
                                res_Images.IsBasec = false;
                                await _unitOfWork.Images.UpdateAsync(item);
                            }
                        }
                    }
                }

                var photoName = $"{Guid.NewGuid()}{Path.GetExtension(request.Photo.FileName)}";

                var path = Path.Combine(_imagePath, photoName);

                using var strem = System.IO.File.Create(path);
                await request.Photo.CopyToAsync(strem);
                //request.Photo.FileName = photoName;



                Image image = _mapper.Map<Image>(request);
                image.Photo = photoName;
                var res = await _unitOfWork.Images.AddAsync(image);
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


        [HttpDelete]
        [Route("DeleteImage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var image = await _unitOfWork.Images.FindAsync(id);
                if (image != null)
                {
                    var path = Path.Combine(_imagePath, image.Photo);
                     System.IO.File.Delete(path);
                    

                    await _unitOfWork.Images.DeleteAsync(image);
                }
                else
                {
                    return Ok("the image is not exists");
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
        [Route("GetAllImagesForProduct")]
        public async Task<IActionResult> GetAllImagesForProduct(int id)
        {
            try
            {
                var res = await _unitOfWork.Images.FindListAsync(x => x.productId == id, null, null);
                if (res == null)
                {
                    return Ok("Not Found Image for this product");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBaseImageForProduct")]
        public async Task<IActionResult> GetBaseImageForProduct(int id)
        {
            try
            {
                var res = await _unitOfWork.Images.FindSingleAsync(x => x.productId == id && x.IsBasec == true);
                if (res == null)
                {
                    return Ok("Not Found base Image for this product");
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
