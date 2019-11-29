namespace rawdata_portfolioproject_2.Models
{
    public class UpdatePasswordDto
    {
        public string Email { get; set; }
        public  string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}