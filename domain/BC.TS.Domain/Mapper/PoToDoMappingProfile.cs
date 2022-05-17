using AutoMapper;
using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;

namespace BC.TS.Domain.Mapper
{
    public class PoToDoMappingProfile : Profile
    {
        public PoToDoMappingProfile()
        {
            CreateMap<CandidatePo, Candidate>();
            CreateMap<ExaminerPo, Examiner>()
                .ForMember(s => s.CandidatesIndex, a => a.Ignore());
        }
    }
}
