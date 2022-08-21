namespace CoffeeRoastery.BLL.Interface.Dto;

public class JwtTokenResponse
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; }
}