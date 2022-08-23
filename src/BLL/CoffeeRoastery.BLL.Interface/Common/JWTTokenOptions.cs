namespace CoffeeRoastery.BLL.Interface.Common;

public class JWTTokenOptions
{
    public const string ConfigurationKey = "JwtToken";

    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}