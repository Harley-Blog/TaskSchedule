using AutoMapper;
using BC.TS.Application.Dto;
using BC.TS.Domain.Dispatcher.Entity;

namespace BC.TS.Application.Mapper
{
    public class DoToDtoMappingProfile : Profile
    {
        public DoToDtoMappingProfile()
        {
            CreateMap<Candidate, CandidateDto>();
            CreateMap<Examiner, ExaminerDto>();
        }
    }
}
