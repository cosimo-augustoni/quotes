namespace quotes_web.Authentication.Domain;

public class OIDCConfiguration
{
    public required string Authority { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}