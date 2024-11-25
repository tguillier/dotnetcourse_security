namespace UserManager.Api.Options
{
    public class JwtAuthenticationOptions
    {
        public const string SectionName = "Authentication";
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Subject { get; set; }
        public required string SecretKey { get; set; }
    }
}
