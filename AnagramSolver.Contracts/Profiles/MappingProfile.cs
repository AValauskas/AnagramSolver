using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Word, WordModel>()
                .ForMember(dest =>
                    dest.LanguagePart,
                    opt => opt.MapFrom(src => src.Category))
                .ForMember(dest =>
                    dest.Word,
                    opt => opt.MapFrom(src => src.Word1))
                .ReverseMap();
            CreateMap<EF.DatabaseFirst.Models.UserLog, Contracts.Models.UserLog>()
                 .ForMember(dest =>
                    dest.Word,
                    opt => opt.MapFrom(src => src.SearchedWord))
               .ReverseMap();
            CreateMap<EF.DatabaseFirst.Models.CachedWord, Contracts.Models.CachedWord>()
             .ReverseMap();
        }
        
    }
}
