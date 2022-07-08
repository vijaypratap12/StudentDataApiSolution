namespace StudentAPI.Model
{
    public class JwtAuthResponse
    {
        public string token { get; set; }
        public string Email { get; set; }
        public int expires_in { get; set; }
    }
}
