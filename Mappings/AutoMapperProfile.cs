using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPIvsJWT.Models;
using TestAPIvsJWT.ViewModel;

namespace DemoDotNet5.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product,ProductViewModel >().ReverseMap();
          
        }
    }
}
