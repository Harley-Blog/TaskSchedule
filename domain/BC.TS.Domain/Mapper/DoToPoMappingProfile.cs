using AutoMapper;
using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;

namespace BC.TS.Domain.Mapper
{
    public class DoToPoMappingProfile : Profile
    {
        public DoToPoMappingProfile()
        {
            CreateMap<Candidate, CandidatePo>();
            CreateMap<Examiner, ExaminerPo>()
                .ForMember(s => s.CandidatesIndex, a => a.Ignore());
        }
    }
}
