using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.WebApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WordEntity, WordModel>()
                .ForMember(dest =>
                    dest.LanguagePart,
                    opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<UserLogEntity, UserLog>()
                 .ForMember(dest =>
                    dest.Word,
                    opt => opt.MapFrom(src => src.SearchedWord))
                  .ForMember(dest =>
                    dest.TaskType,
                    opt => opt.MapFrom(src => src.Type))
               .ReverseMap();

            CreateMap<CachedWordEntity, CachedWord>()
             .ReverseMap();
        }
        
    }
}
