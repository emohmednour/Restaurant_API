namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "Nationality";
}

public static class AppClaimsType
{
    public const string Nationality = "Nationality";
    public const string DataOfBirth = "DataOfBirth";
}
