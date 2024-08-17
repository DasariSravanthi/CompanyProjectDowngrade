using Mapster;
using Newtonsoft.Json;

using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.Create;
using CompanyApp.Models.DTO.Update;
using CompanyApp.Models.DTO.User;
using CompanyApp.Converter;

namespace CompanyApp.Mapper.Configuration;

public static class MapsterConfiguration
{
    public static void ConfigureMappings(TypeAdapterConfig config)
    {
        
        config.NewConfig<ProductDto, Product>();

        config.NewConfig<ProductDetailDto, ProductDetail>();

        config.NewConfig<SupplierDto, Supplier>();

        config.NewConfig<SizeDto, Size>();

        config.NewConfig<ProductStockDto, ProductStock>();

        config.NewConfig<ReceiptDto, Receipt>()
              .Map(dest => dest.ReceiptDate, src => DateOnly.ParseExact(src.ReceiptDate, "yyyy-MM-dd"))
              .Map(dest => dest.BillDate, src => DateOnly.ParseExact(src.BillDate, "yyyy-MM-dd"));

        config.NewConfig<ReceiptDetailDto, ReceiptDetail>();

        config.NewConfig<RollNumberDto, RollNumber>();

        config.NewConfig<IssueDto, Issue>()
              .Map(dest => dest.IssueDate, src => DateOnly.ParseExact(src.IssueDate, "yyyy-MM-dd"));

        config.NewConfig<ProductionCoatingDto, ProductionCoating>()
              .Map(dest => dest.ProductionCoatingDate, src => DateOnly.ParseExact(src.ProductionCoatingDate, "yyyy-MM-dd"))
              .Map(dest => dest.CoatingStart, src => TimeOnly.Parse(src.CoatingStart))
              .Map(dest => dest.CoatingEnd, src => TimeOnly.Parse(src.CoatingEnd));

        config.NewConfig<ProductionCalendaringDto, ProductionCalendaring>()
              .Map(dest => dest.ProductionCoatingDate, src => DateOnly.ParseExact(src.ProductionCoatingDate, "yyyy-MM-dd"))
              .Map(dest => dest.CalendaringStart, src => TimeOnly.Parse(src.CalendaringStart))
              .Map(dest => dest.CalendaringEnd, src => TimeOnly.Parse(src.CalendaringEnd));

        config.NewConfig<ProductionSlittingDto, ProductionSlitting>()
              .Map(dest => dest.ProductionCoatingDate, src => DateOnly.ParseExact(src.ProductionCoatingDate, "yyyy-MM-dd"))
              .Map(dest => dest.SlittingStart, src => TimeOnly.Parse(src.SlittingStart))
              .Map(dest => dest.SlittingEnd, src => TimeOnly.Parse(src.SlittingEnd));

        config.NewConfig<SlittingDetailDto, SlittingDetail>();

         config.NewConfig<UpdateProductDto, Product>()
               .IgnoreNullValues(true);   // Ignore null values;

        config.NewConfig<UpdateProductDetailDto, ProductDetail>()
              .IgnoreNullValues(true);

        config.NewConfig<UpdateSupplierDto, Supplier>()
              .IgnoreNullValues(true);

        config.NewConfig<UpdateSizeDto, Size>()
              .IgnoreNullValues(true);

        config.NewConfig<UpdateProductStockDto, ProductStock>()
              .IgnoreNullValues(true);

        config.NewConfig<UpdateReceiptDto, Receipt>()
              .Map(dest => dest.ReceiptDate, src => DeserializeDateOnly(src.ReceiptDate))
              .Map(dest => dest.BillDate, src => DeserializeDateOnly(src.BillDate))
              .IgnoreNullValues(true);

        config.NewConfig<UpdateReceiptDetailDto, ReceiptDetail>()
              .IgnoreNullValues(true);

        config.NewConfig<UpdateRollNumberDto, RollNumber>()
              .IgnoreNullValues(true);

        config.NewConfig<UpdateIssueDto, Issue>()
              .Map(dest => dest.IssueDate, src => DeserializeDateOnly(src.IssueDate))
              .IgnoreNullValues(true);

        config.NewConfig<UpdateProductionCoatingDto, ProductionCoating>()
              .Map(dest => dest.ProductionCoatingDate, src => DeserializeDateOnly(src.ProductionCoatingDate))
              .Map(dest => dest.CoatingStart, src => DeserializeTimeOnly(src.CoatingStart))
              .Map(dest => dest.CoatingEnd, src => DeserializeTimeOnly(src.CoatingEnd))
              .IgnoreNullValues(true);

        config.NewConfig<UpdateProductionCalendaringDto, ProductionCalendaring>()
              .Map(dest => dest.ProductionCoatingDate, src => DeserializeDateOnly(src.ProductionCoatingDate))
              .Map(dest => dest.CalendaringStart, src => DeserializeTimeOnly(src.CalendaringStart))
              .Map(dest => dest.CalendaringEnd, src => DeserializeTimeOnly(src.CalendaringEnd))
              .IgnoreNullValues(true);

        config.NewConfig<UpdateProductionSlittingDto, ProductionSlitting>()
              .Map(dest => dest.ProductionCoatingDate, src => DeserializeDateOnly(src.ProductionCoatingDate))
              .Map(dest => dest.SlittingStart, src => DeserializeTimeOnly(src.SlittingStart))
              .Map(dest => dest.SlittingEnd, src => DeserializeTimeOnly(src.SlittingEnd))
              .IgnoreNullValues(true);

        config.NewConfig<UpdateSlittingDetailDto, SlittingDetail>()
              .IgnoreNullValues(true);


        config.NewConfig<RegisterUserDto, User>();
        
    }

    private static DateOnly? DeserializeDateOnly(string? dateString)
    {
        if (string.IsNullOrEmpty(dateString))
            return null;
            
        if (DateOnly.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var date)) 
        {
            return date;
        }

        throw new JsonException($"Unable to convert \"{dateString}\" to DateOnly.");
    }

    private static string? SerializeDateOnly(DateOnly? date)
    {
        if (!date.HasValue)
            return null;

        // Use Newtonsoft.Json to serialize the DateOnly value
        return JsonConvert.SerializeObject(date, new NullableDateOnlyJsonConverter());
    }

    private static TimeOnly? DeserializeTimeOnly(string? timeString)
    {
        if (string.IsNullOrEmpty(timeString))
            return null;

            if (TimeOnly.TryParseExact(timeString, "HH:mm", null, System.Globalization.DateTimeStyles.None, out var time))
        {
            return time;
        }

        throw new JsonException($"Unable to convert \"{timeString}\" to TimeOnly.");
    }

    private static string? SerializeTimeOnly(TimeOnly? time)
    {
        if (!time.HasValue)
            return null;

        // Use Newtonsoft.Json to serialize the TimeOnly value
        return JsonConvert.SerializeObject(time, new NullableTimeOnlyJsonConverter());
    }
}