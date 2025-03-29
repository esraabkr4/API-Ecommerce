using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Shared;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDTO>()
                .ForMember(d => d.BrandName, (options) => options.MapFrom(s => s.productBrand.Name))
                .ForMember(d=>d.TypeName,(options)=>options.MapFrom(s=>s.productType.Name))
                .ForMember(d => d.PictureUrl, (options) => options.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand,BrandResultDTO>();
            CreateMap<ProductType,TypeResultDTO>();
        }
    }
}
