using FullStack.Domain.Entities;
using FullStack.Api.Contracts.Schemas;
using FullStack.Core.Infrastructure.Mappings;

namespace FullStack.Api.Infrastructure.Mappings
{
    public class AirlineMapping : GenericMapping<Airline>
    {
        #region Constructors

        public AirlineMapping()
            : base(nameof(AirlineMapping))
        { }

        #endregion

        #region Overriden Methods

        public override void Configure()
        {
            CreateMap<Airline, AirlineSchema>()
                .ForMember(dest => dest.AirlineId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ICAO, opts => opts.MapFrom(src => src.ICAO))
                .ForMember(dest => dest.IATA, opts => opts.MapFrom(src => src.IATA))
                .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.Image))
                .ForMember(dest => dest.Callsign, opts => opts.MapFrom(src => src.Callsign))
                .ForAllOtherMembers(opts => opts.Ignore());
        }

        #endregion
    }
}
