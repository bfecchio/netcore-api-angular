using FullStack.Domain.Entities;
using FullStack.Api.Contracts.Schemas;
using FullStack.Api.Contracts.Requests;
using FullStack.Core.Infrastructure.Mappings;

namespace FullStack.Api.Infrastructure.Mappings
{
    public class TicketMapping : GenericMapping<Ticket>
    {
        #region Constructors

        public TicketMapping()
            : base(nameof(TicketMapping))
        { }

        #endregion

        #region Overriden Methods

        public override void Configure()
        {
            CreateMap<Ticket, TicketSchema>()
                .ForMember(dest => dest.TicketId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Passenger, opts => opts.MapFrom(src => src.Passenger))
                .ForMember(dest => dest.Flight, opts => opts.MapFrom(src => src.Flight))
                .ForMember(dest => dest.Gate, opts => opts.MapFrom(src => src.Gate))
                .ForMember(dest => dest.Scheduled, opts => opts.MapFrom(src => src.Scheduled))
                .ForMember(dest => dest.Airline, opts => opts.MapFrom(src => src.Airline))
                .ForMember(dest => dest.Origin, opts => opts.MapFrom(src => src.Origin))
                .ForMember(dest => dest.Destination, opts => opts.MapFrom(src => src.Destination))
                .ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<TicketPostRequest, Ticket>()                
                .ForMember(dest => dest.Passenger, opts => opts.MapFrom(src => src.Passenger))
                .ForMember(dest => dest.Flight, opts => opts.MapFrom(src => src.Flight))
                .ForMember(dest => dest.Gate, opts => opts.MapFrom(src => src.Gate))
                .ForMember(dest => dest.Scheduled, opts => opts.MapFrom(src => src.Scheduled))
                .ForMember(dest => dest.AirlineId, opts => opts.MapFrom(src => src.AirlineId))
                .ForMember(dest => dest.OriginId, opts => opts.MapFrom(src => src.OriginId))
                .ForMember(dest => dest.DestinationId, opts => opts.MapFrom(src => src.DestinationId))
                .ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<TicketPutRequest, Ticket>()
                .ForMember(dest => dest.Passenger, opts => opts.MapFrom(src => src.Passenger))
                .ForMember(dest => dest.Flight, opts => opts.MapFrom(src => src.Flight))
                .ForMember(dest => dest.Gate, opts => opts.MapFrom(src => src.Gate))
                .ForMember(dest => dest.Scheduled, opts => opts.MapFrom(src => src.Scheduled))
                .ForMember(dest => dest.AirlineId, opts => opts.MapFrom(src => src.AirlineId))
                .ForMember(dest => dest.OriginId, opts => opts.MapFrom(src => src.OriginId))
                .ForMember(dest => dest.DestinationId, opts => opts.MapFrom(src => src.DestinationId))
                .ForAllOtherMembers(opts => opts.Ignore());
        }

        #endregion
    }
}
