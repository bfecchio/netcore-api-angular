using FullStack.Domain.Entities;
using FullStack.Api.Contracts.Schemas;
using FullStack.Core.Infrastructure.Mappings;

namespace FullStack.Api.Infrastructure.Mappings
{
    public class AirportMapping : GenericMapping<Airport>
    {
        #region Constructors

        public AirportMapping()
            : base(nameof(AirportMapping))
        { }

        #endregion

        #region Overriden Methods

        public override void Configure()
        {
            CreateMap<Airport, AirportSchema>()
                .ForMember(dest => dest.AirportId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State))
                .ForMember(dest => dest.ICAO, opts => opts.MapFrom(src => src.ICAO))
                .ForMember(dest => dest.IATA, opts => opts.MapFrom(src => src.IATA))
                .ForAllOtherMembers(opts => opts.Ignore());
        }

        #endregion
    }
}
