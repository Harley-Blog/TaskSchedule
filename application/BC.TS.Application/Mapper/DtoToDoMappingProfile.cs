using AutoMapper;
using BC.TS.Application.Dto;
using BC.TS.Domain.Dispatcher.Entity;

namespace BC.TS.Application.Mapper
{
    public class DtoToDoMappingProfile : Profile
    {
        public DtoToDoMappingProfile()
        {
            CreateMap<CandidateDto, Candidate>();
            CreateMap<ExaminerDto, Examiner>();
        }
    }
}
