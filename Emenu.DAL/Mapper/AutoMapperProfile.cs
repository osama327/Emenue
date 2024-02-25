using AutoMapper;
using Emenu.Core.Dto.Image;
using Emenu.Core.Dto.Product;
using Emenu.Core.Dto.ProductAttribute;
using Emenu.Core.Dto.Stock;
using Emenu.Core.Dto.Variant;
using Emenu.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.DAL.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductAttributeDto, ProductAttribute>().ReverseMap();
            CreateMap<VariantDto, Variant>().ReverseMap();
            CreateMap<ImageDto, Image>().ReverseMap();
            CreateMap<StockDto, Stock>().ReverseMap();
        }  
    }
}
