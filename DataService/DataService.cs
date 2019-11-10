using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace rawdata_portfolioproject_2
{
    public class DataService : IDataService
    {
        // make functions here
        public Post GetPost(int postId)
        {
            using var db = new StackOverflowContext();
            Post post = db.Posts.Find(postId);
            post.Question = GetQuestion(postId);
            post.Answer = GetAnswer(postId);
            post.User = GetUser(post.UserId);
            return post;
        }
        public Question GetQuestion(int postId)
        {
            using var db = new StackOverflowContext();
            return db.Questions.Find(postId);
        }
        public Link GetLink(int postId, int linkToPostId)
        {
            using var db = new StackOverflowContext();
            return db.Links.Find(postId, linkToPostId);
        }
        public Answer GetAnswer(int postId)
        {
            using var db = new StackOverflowContext();
            return db.Answers.Find(postId);
        }
        public Comment GetComment(int commentId)
        {
            using var db = new StackOverflowContext();
            Comment comment = db.Comments.Find(commentId);
            comment.User = GetUser(comment.UserId);
            return comment;
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            using var db = new StackOverflowContext();
            return db.Comments.Where(x => x.PostId == postId).Select(x => x).ToList();
        }

        public User GetUser(int userId)
        {
            using var db = new StackOverflowContext();
            return db.Users.Find(userId);
        }

        public List<Post> BestMatchSearch(params string[] keywords)
        {
            throw new NotImplementedException(); // how to do this?
        }

        public List<Post> ExactMatchSearch(params string[] keywords)
        {
            throw new NotImplementedException(); // how to do this??
        }

        public List<Post> SimpleSearch(params string[] keywords)
        {
            throw new NotImplementedException(); // how to do this?
        }


        public IList<weighted_inverted_index> RankedWeightSearch(PagingAttributes pagingAttributes, params string[] keywords)
        {
            using var db = new StackOverflowContext();
            var weights = db.weighted_inverted_index.FromSqlRaw("select ranked_weight_variadic({0})", keywords)
                .Take(pagingAttributes.PageSize).ToList()
                .Skip(pagingAttributes.Page * pagingAttributes.PageSize)
                .ToList();
            //var result = db.RankedWeight_Result.FromSqlRaw("select postid, body from posts where postid in {0}),;
            return weights;
        }


        public Profile CreateProfile(string email, string password)
        {
            using var db = new StackOverflowContext();
            try
            {
                db.Database.ExecuteSqlRaw("select create_profile({0}, {1})", email, password);
            }
            catch (Exception)
            {
                // in case of duplicate profile
                return null;
            }

            return GetProfile(email);
        }

        public Profile GetProfile(int profileId)
        {
            using var db = new StackOverflowContext();
            return db.Profiles.Find(profileId);
        }

        public Profile GetProfile(string email)
        {
            using var db = new StackOverflowContext();
            return db.Profiles.Where(x => x.Email == email).Select(x => x).First();
        }

        public bool UpdateProfileEmail(string oldEmail, string password, string newEmail) 
        {
            using var db = new StackOverflowContext();
            Profile profile = db.Profiles.Where(x => x.Email == oldEmail).Select(x => x).ToList().FirstOrDefault();
            
            if (profile == null)
                return false;
            if (!Login(oldEmail, password))
                return false;
            
            profile.Email = newEmail;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProfilePassword(string email, string oldPassword, string newPassword)
        {
            using var db = new StackOverflowContext();
            Profile profile = db.Profiles.Where(x => x.Email == email).Select(x => x).ToList().FirstOrDefault();
            
            if (profile == null)
                return false;
            if (!Login(email, oldPassword))
                return false;

            db.Database.ExecuteSqlRaw("select change_password({0},{1})", email, newPassword);
            return true;
        }

        public bool DeleteProfile(string email, string password)
        {
            using var db = new StackOverflowContext();
            Profile profile = db.Profiles.Where(x => x.Email == email).Select(x => x).ToList().FirstOrDefault();
            
            if (profile == null)
                return false;
            if (!Login(email, password))
                return false;
            
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return true;
        }

        public bool Login(string email, string password)
        {
            using var db = new StackOverflowContext();
            var result = db.Profile_LoginResults.FromSqlRaw("select profile_login({0},{1})", email, password).ToList().First();
            return result.Result;
        }

        public Query CreateQuery(int profileId, DateTime timeSearched, string queryText)
        {
            using var db = new StackOverflowContext();
            Query query = new Query();
            query.ProfileId = profileId;
            query.TimeSearched = timeSearched;
            query.QueryText = queryText;
            db.Queries.Add(query);
            db.SaveChanges();
            return query;
        }

        public Query GetQuery(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Find(profileId, timeSearched);
        }

        public List<Query> GetAllQueries(int profileId)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Where(x => x.ProfileId == profileId).Select(x => x).ToList();
        }

        public List<Query> GetQueriesBefore(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Where(x => x.TimeSearched.CompareTo(timeSearched) < 0).Select(x => x).ToList();
        }

        public List<Query> GetQueriesAfter(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Where(x => x.TimeSearched.CompareTo(timeSearched) > 0).Select(x => x).ToList();
        }

        public List<Query> GetQueriesByString(int profileId, params string[] keywords)
        {
            using var db = new StackOverflowContext();
            List<Query> queries = new List<Query>();
            foreach (var keyword in keywords) // smarter way to do this with lambda functions??
            {
                foreach (var query in db.Queries.ToList())
                {
                    if (query.QueryText.Contains(keyword))
                        queries.Add(query);
                }
            }
            return queries;
        }

        public bool DeleteQuery(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            Query query = db.Queries.Find(profileId, timeSearched);

            if (query == null)
                return false;

            db.Queries.Remove(query);
            db.SaveChanges();
            return true;
        }

        public bool DeleteQueries(List<Query> queries)
        {
            using var db = new StackOverflowContext();
            foreach (var query in queries)
            {
                db.Queries.Remove(query);
            }

            db.SaveChanges();
            return true; // make void or check for nulls
        }

        public Bookmark CreateBookmark(int profileId, int postId, string note)
        {
            using var db = new StackOverflowContext();
            Bookmark bookmark = new Bookmark();
            bookmark.ProfileId = profileId;
            bookmark.PostId = postId;
            bookmark.Note = note;
            db.Bookmarks.Add(bookmark);
            db.SaveChanges();
            return bookmark;
        }
        
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            using var db = new StackOverflowContext();
            db.Bookmarks.Add(bookmark);
            db.SaveChanges();
            return bookmark;
        }

        public Bookmark GetBookmark(int profileId, int postId)
        {
            using var db = new StackOverflowContext();
            return db.Bookmarks.Find(profileId, postId);
        }

        public List<Bookmark> GetAllBookmarks(int profileId)
        {
            using var db = new StackOverflowContext();
            return db.Bookmarks.Where(x => x.ProfileId == profileId).Select(x => x).ToList();
        }

        public List<Bookmark> GetBookmarksByString(int profileId, params string[] keywords)
        {
            using var db = new StackOverflowContext();
            List<Bookmark> bookmarks = new List<Bookmark>();
            foreach (var keyword in keywords) // smarter way to do this with lambda functions??
            {
                foreach (var bookmark in db.Bookmarks.ToList())
                {
                    if (bookmark.Note.Contains(keyword))
                        bookmarks.Add(bookmark);
                }
            }
            return bookmarks;
        }

        public bool UpdateBookmark(int profileId, int postId, string note)
        {
            using var db = new StackOverflowContext();
            Bookmark bookmark = db.Bookmarks.Find(profileId, postId);

            if (bookmark == null)
                return false;

            bookmark.Note = note;
            db.SaveChanges();
            return true;
        }

        public bool DeleteBookmark(int profileId, int postId)
        {
            using var db = new StackOverflowContext();
            Bookmark bookmark = db.Bookmarks.Find(profileId, postId);

            if (bookmark == null)
                return false;

            db.Bookmarks.Remove(bookmark);
            db.SaveChanges();
            return true;
        }

        public bool DeleteBookmarks(List<Bookmark> bookmarks)
        {
            using var db = new StackOverflowContext();
            foreach (var bookmark in bookmarks)
            {
                db.Bookmarks.Remove(bookmark);
            }

            db.SaveChanges();
            return true; // make void or check if any nulls
        }
    }
}