using CompanyApp.Models.DTO.Update;
using CompanyApp.Models.Entity;

namespace CompanyApp.Mapper;

public static class ManualMapper
{
    public static void Map(UpdateSupplierDto source, Supplier destination)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        // Map StringValue if it's not null
        if (source.SupplierName != null)
        {
            destination.SupplierName = source.SupplierName;
        }

        // Map NullableIntValue to NonNullableIntValue if it's not null
        if (source.Dues.HasValue)
        {
            destination.Dues = source.Dues.Value;
        }
    }
}