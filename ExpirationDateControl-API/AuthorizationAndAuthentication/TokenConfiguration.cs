namespace ExpirationDateControl_API.AuthorizationAndAuthentication
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpirationtimeInHours { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public string Module { get; set; }
        public string UserName { get; set; }
    }
}
