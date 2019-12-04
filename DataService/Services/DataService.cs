using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class DataService : IDataService
    {
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