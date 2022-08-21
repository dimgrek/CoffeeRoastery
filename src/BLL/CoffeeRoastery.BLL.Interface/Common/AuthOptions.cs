namespace CoffeeRoastery.BLL.Interface.Common;

public class AuthOptions
{
    public const string ConfigurationKey = "Auth";
    public string UserName { get; set; }
    public string Password { get; set; }
}