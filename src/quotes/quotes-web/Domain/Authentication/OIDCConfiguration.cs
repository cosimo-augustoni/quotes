namespace quotes_web.Domain.Authentication;

public class OIDCConfiguration
{
    public required string Authority { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}