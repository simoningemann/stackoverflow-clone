using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services.Interfaces
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(int profileId, int postId, string note);
        Bookmark GetBookmark(int bookmarkId);
        List<Bookmark> GetAllBookmarks(int profileId);
        List<Bookmark> GetBookmarksByString(int profileId, params string[] keywords);
        Bookmark UpdateBookmark(int bookmarkId, string note);
        Bookmark DeleteBookmark(int bookmarkId);
        List<Bookmark> DeleteAllBookmarks(int profileId);
    }
}