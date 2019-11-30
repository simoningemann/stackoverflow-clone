using System;
using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public interface IDataService
    {
        Link GetLink(int postId, int linkToPostId);
        Comment GetComment(int commentId);
        List<Comment> GetCommentsByPostId(int postId);
        User GetUser(int userId);
        List<Post> BestMatchSearch(params string[] keywords);
        List<Post> ExactMatchSearch(params string[] keywords);
        List<Post> SimpleSearch(params string[] keywords);
        Profile CreateProfile(string email, string password);
        Profile GetProfile(int profileId);
        Profile GetProfile(string email);
        bool UpdateProfileEmail(string oldEmail, string password, string newEmail);
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