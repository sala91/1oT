namespace _1oT.Models
{
    public class Authorize
    {
        // guid maybe?
        public string access_token { get; set; }
        public string token_type { get; set; }
        // guid maybe?
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }
}
