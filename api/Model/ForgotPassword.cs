namespace api.Model
{
    public class ForgotPassword
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool isEnabled { get; set; } = true;
    }
}
