using Mapster;

namespace CompanyApp.Mapper.MapperService;

public class AppMapper
{
    private readonly TypeAdapterConfig _config;

    public AppMapper(TypeAdapterConfig config)
    {
        _config = config;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return source.Adapt<TSource, TDestination>(_config);
    }

    public void Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        source.Adapt(destination, _config);
    }

    // public Supplier Map(UpdateSupplierDto source)
    // {
    //     TypeAdapterConfig<UpdateSupplierDto, Supplier>.NewConfig().IgnoreNullValues(true);

    //     var dest = source.Adapt<Supplier>();

    //     return dest;
    // }

    // public void UpdateDestination(UpdateSupplierDto source, Supplier destination)
    // {
    //     // Perform the mapping from source to the existing destination object
    //      if (source == null || destination == null) return;

    //     var config = new TypeAdapterConfig();
    //     config.ForType<UpdateSupplierDto, Supplier>()
    //           .IgnoreNullValues(true); // Custom configuration to ignore null values

    //     source.Adapt(destination, config);
    // }
}