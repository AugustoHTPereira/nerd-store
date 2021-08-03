namespace NSE.Core.Services.Identity
{
    public class TokenOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }

        /// <summary>
        /// Expiration value in hours
        /// </summary>
        public int Expiress { get; set; }
        public string Audience { get; set; }
    }
}
