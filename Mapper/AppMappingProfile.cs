using AutoMapper;

using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;

namespace CompanyApp.Mapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // CreateMap<Source, Destination>();
        CreateMap<ProductDto, Product>();

        CreateMap<ProductDetailDto, ProductDetail>();

        CreateMap<SupplierDto, Supplier>();

        CreateMap<SizeDto, Size>();

        CreateMap<ProductStockDto, ProductStock>();

        CreateMap<ReceiptDto, Receipt>()
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.ReceiptDate, "yyyy-MM-dd")))
            .ForMember(dest => dest.BillDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.BillDate, "yyyy-MM-dd")));

        CreateMap<ReceiptDetailDto, ReceiptDetail>();

        CreateMap<RollNumberDto, RollNumber>();

        CreateMap<IssueDto, Issue>()
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.IssueDate, "yyyy-MM-dd")));

        CreateMap<ProductionCoatingDto, ProductionCoating>()
            .ForMember(dest => dest.ProductionCoatingDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.ProductionCoatingDate, "yyyy-MM-dd")))
            .ForMember(dest => dest.CoatingStart, opt => opt.MapFrom(src => TimeOnly.Parse(src.CoatingStart)))
            .ForMember(dest => dest.CoatingEnd, opt => opt.MapFrom(src => TimeOnly.Parse(src.CoatingEnd)));

        CreateMap<ProductionCalendaringDto, ProductionCalendaring>()
            .ForMember(dest => dest.ProductionCoatingDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.ProductionCoatingDate, "yyyy-MM-dd")))
            .ForMember(dest => dest.CalendaringStart, opt => opt.MapFrom(src => TimeOnly.Parse(src.CalendaringStart)))
            .ForMember(dest => dest.CalendaringEnd, opt => opt.MapFrom(src => TimeOnly.Parse(src.CalendaringEnd)));

        CreateMap<ProductionSlittingDto, ProductionSlitting>()
            .ForMember(dest => dest.ProductionCoatingDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.ProductionCoatingDate, "yyyy-MM-dd")))
            .ForMember(dest => dest.SlittingStart, opt => opt.MapFrom(src => TimeOnly.Parse(src.SlittingStart)))
            .ForMember(dest => dest.SlittingEnd, opt => opt.MapFrom(src => TimeOnly.Parse(src.SlittingEnd)));

        CreateMap<SlittingDetailDto, SlittingDetail>();

        CreateMap<UpdateSupplierDto, Supplier>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));    // Ignore null values;
        
    }
}