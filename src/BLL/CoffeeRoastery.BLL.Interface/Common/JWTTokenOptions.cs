namespace webapi.Options;

public class JWTTokenOptions
{
    public const string ConfigurationKey = "JwtToken";

    public string Secret { get; set; }
}