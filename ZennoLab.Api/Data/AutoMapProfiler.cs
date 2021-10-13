using AutoMapper;
using ZennoLab.Api.Model;

namespace ZennoLab.Api.Data
{
    /// <summary>
    /// Профайлер Mapper
    /// </summary>
    public class AutoMapProfiler : Profile
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="AutoMapProfiler"/>
        /// </summary>
        public AutoMapProfiler()
        {
            CreateMap<DataSet, DataSetEntity>()
                .ForMember(x => x.LocationAnswer, o => o.MapFrom(x => x.LocationAnswer.ToString()))
                .ForMember(x => x.ArchivePath, o => o.Ignore());
        }
    }
}
