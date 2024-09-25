using AdCreative.Domain;
using AdCreative.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdCreative.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WordAdd, WordDto>();
            CreateMap<WordDto, WordAdd>();
        }
    }
}
