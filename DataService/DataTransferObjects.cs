using System;
using System.Collections.Generic;

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
        public  string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdateEmailDto
    {
        public string OldEmail { get; set; }
        public  string Password { get; set; }
        public string NewEmail { get; set; }
    }

    public class CreateOrUpdateBookmarkDto
    {
        public int ProfileId { get; set; }
        public int BookmarkId { get; set; }
        public string Note { get; set; }
    }

    public class DeleteBookmarkDto
    {
        public int ProfileId { get; set; }
        public int BookmarkId { get; set; }
    }

    public class CreateQueryDto
    {
        public int ProfileId { get; set; }
        public DateTime TimeSearched { get; set; }
        public string QueryText { get; set; }
    }

    public class DeleteQueryDto
    {
        public int ProfileId { get; set; }
        public DateTime TimeSearched { get; set; }
    }

    public class RankedSearchDto
    {
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string[] Keywords { get; set; }
    }
    
    public class RankedSearchResultDto
    {
        public int TotalQuestions { get; set; }
        public int TotalPages { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }

    
    public class QuestionDto
    {
        public string Link { get; set; }
        public Post Question { get; set;}
    }
}