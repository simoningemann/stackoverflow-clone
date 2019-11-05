using System;
using System.Collections.Generic;
using System.Dynamic;

namespace rawdata_portfolioproject_2
{
    public interface IDataService
    {
        // THESE SUGGESTIONS ARE ONLY PRELIMINARY. ADD/REMOVE/REFINE FUNCTIONS AS NEEDED
        
        // PROFILE FUNCTIONS
        // create/read/update/delete (CRUD) profile
        // login/out
        // CRUD bookmarkLink
        
        // QA FUNCTIONS
        // get post (single or multiple) (by id or searchwords)
        // get question (single) (by postid)
        // get link (multiple) by (by postid and linktoid)
        // get answer (multiple) (by postid and answertoid)
        // get comment (multiple) (by postid)
        // get user (single) (by userid)
        
        // IMPORTANT QUESTION: HOW TO USE STORED PROCEDURES?? ARE WE SUPPOSED TO USE THE STORED PROCEDURES???
        Post GetPost(int postId);
        Question GetQuestion(int postId);
        Link GetLink(int postId, int linkToPostId);
        Answer GetAnswer(int postId);
        Comment GetComment(int commentId);
        List<Comment> GetCommentsByPostId(int postId);
        User GetUser(int userId);
        List<Post> BestMatchSearch(params string[] keywords);
        List<Post> ExactMatchSearch(params string[] keywords);
        List<Post> SimpleSearch(params string[] keywords);
        List<Post> RankedWeightSearch(params string[] keywords);
        Profile CreateProfile(string email, string password);
        Profile GetProfile(int profileId);
        Profile GetProfile(string email);
        bool UpdateProfileEmail(int profileId, string email);
        bool UpdateProfilePassword (string email, string oldPassword, string newPassword);
        bool DeleteProfile(string email, string password);
        bool Login(string email, string password);
        Query CreateQuery(int profileId, DateTime timeSearched, string queryText);
        Query GetQuery(int profileId, DateTime timeSearched);
        List<Query> GetAllQueries(int profileId);
        List<Query> GetQueriesBefore(int profileId, DateTime timeSearched);
        List<Query> GetQueriesAfter(int profileId, DateTime timeSearched);
        List<Query> GetQueriesByString(int profileId, params string[] keywords);
        bool DeleteQuery(int profileId, DateTime timeSearched);
        bool DeleteQueries(List<Query> queries);
        Bookmark CreateBookmark(int profileId, int postId, string note);
        Bookmark CreateBookmark(Bookmark bookmark);
        Bookmark GetBookmark(int profileId, int postId);
        List<Bookmark> GetAllBookmarks(int profileId);
        List<Bookmark> GetBookmarksByString(int profileId, params string[] keywords);
        bool UpdateBookmark(int profileId, int postId, string note);
        bool DeleteBookmark(int profileId, int postId);
        bool DeleteBookmarks(List<Bookmark> bookmarks);
    }
}