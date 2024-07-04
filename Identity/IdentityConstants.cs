using System.Collections.ObjectModel;

namespace CompanyApp.Identity;

public class IdentityConstants {
    public const string PolicyName1 = "Admin";

    public const string PolicyName2 = "User";

    public static readonly ReadOnlyCollection<string> ClaimNames1 = new ReadOnlyCollection<string>(
        new List<string> { "admin", "manager"}
    );
}