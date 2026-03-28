namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
    public const string AtLeast2Restaurants = "AtLeast2Restaurants";
}

public static class AppClaimsType
{
    public const string Nationality = "Nationality";
    public const string DataOfBirth = "DataOfBirth";
}
