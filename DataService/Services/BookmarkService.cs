using System;
using System.Collections.Generic;
using System.Linq;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class BookmarkService : IBookmarkService
    {
        public Bookmark CreateBookmark(int profileId, int postId, string note)
        {
            using var db = new StackOverflowContext();
            Bookmark bookmark = new Bookmark();
            bookmark.BookmarkId = NextBookmarkId(db);
            bookmark.ProfileId = profileId;
            bookmark.PostId = postId;
            bookmark.Note = note;

            try
            {
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();
                return bookmark;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Bookmark GetBookmark(int bookmarkId)
        {
            using var db = new StackOverflowContext();
            return db.Bookmarks.Find(bookmarkId);
        }

        public List<Bookmark> GetAllBookmarks(int profileId)
        {
            using var db = new StackOverflowContext();
            var bookmarks = db.Bookmarks.Where(x => x.ProfileId == profileId).Select(x => x).ToList();
            
            return bookmarks;
        }

        public List<Bookmark> GetBookmarksByString(int profileId, params string[] keywords)
        {
            using var db = new StackOverflowContext();
            var bookmarks = db.Bookmarks.Where((x, y) => x.Note.Contains(keywords[y])).Select(x => x).ToList();
            
            return bookmarks;
            /*List<Bookmark> bookmarks = new List<Bookmark>();
            foreach (var keyword in keywords) // smarter way to do this with lambda functions??
            {
                foreach (var bookmark in db.Bookmarks.ToList())
                {
                    if (bookmark.Note.Contains(keyword))
                        bookmarks.Add(bookmark);
                }
            }*/
        }

        public Bookmark UpdateBookmark(int bookmarkId, int profileId, string note)
        {
            using var db = new StackOverflowContext();
            var bookmark = db.Bookmarks.Find(bookmarkId);
            
            if (bookmark == null) return null;
            if (bookmark.ProfileId != profileId) return null;

            try
            {
                bookmark.Note = note;
                db.SaveChanges();
                return bookmark;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Bookmark DeleteBookmark(int bookmarkId, int profileId)
        {
            using var db = new StackOverflowContext();
            var bookmark = db.Bookmarks.Find(bookmarkId);

            if (bookmark == null) return null;
            if (profileId != bookmark.ProfileId) return null;

            try
            {
                db.Bookmarks.Remove(bookmark);
                db.SaveChanges();
                return bookmark;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Bookmark> DeleteAllBookmarks(int profileId)
        {
            using var db = new StackOverflowContext();
            var bookmarks = db.Bookmarks.Where(x => x.ProfileId == profileId).Select(x => x).ToList();
            foreach (var bookmark in bookmarks)
            {
                db.Bookmarks.Remove(bookmark);
            }

            db.SaveChanges();
            return bookmarks;
        }

        private int NextBookmarkId(StackOverflowContext db)
        {
            var id = 1;
            var ids = db.Bookmarks.Select(x => x.BookmarkId);

            while (ids.Contains(id))
            {
                id = id + 1;
            }

            return id;
        } 
    }
}