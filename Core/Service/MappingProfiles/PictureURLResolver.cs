using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;

namespace Service.MappingProfiles
{
    internal class PictureURLResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty; // or return a default image URL
            }
            else
            {
                //https://localhost:7267/{source.PictureUrl}
                var url = $"{configuration.GetSection("URLs")["BaseUrl"]}{source.PictureUrl}";
                return url;
            }
        }
    }
}
