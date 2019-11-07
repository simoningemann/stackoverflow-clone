namespace rawdata_portfolioproject_2
{
    public class LoginDto
    {
        public string Email { get; set; }
        public  string Password { get; set; }
    }

    public class UpdatePasswordDto
    {
        public string Email { get; set; }
        public  string Password { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdateEmailDto
    {
        public string Email { get; set; }
        public  string Password { get; set; }
        public string NewEmail { get; set; }
    }
}