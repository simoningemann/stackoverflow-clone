namespace rawdata_portfolioproject_2
{
    // IMPORTANT CHOOSE WHICH ATTRIBUTES TO INCLUDE: HERE ARE SOME POTENTIAL ONES
    public class Post
    {
        // id
        // creationdata
        // score
        // body
        // userid
    }
    
    public class Question
    {
        // postid
        // title
        // tags
        // acceptedanswerid
    }
    
    public class Link
    {
        // postid
        // linktopostid
    }
    
    public class Answer
    {
        // postid
        // answertoid
    }
    
    public class Comment
    {
        // commentid
        // postid
        // creationdate
        // score
        // body
        // userid
    }
    
    public class User
    {
        // userid
        // name
        // creationdate
        // location
        // age
    }
    
    public class Word // check actual inverted index in db... is this needed in DAL?
    {
        
    }
}