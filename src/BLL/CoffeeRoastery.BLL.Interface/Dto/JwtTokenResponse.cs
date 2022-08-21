namespace CoffeeRoastery.BLL.Interface.Dto;

public class JwtTokenResponse
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public string TokenType { get; set; }
}