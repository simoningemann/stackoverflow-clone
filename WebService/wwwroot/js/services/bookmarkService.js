define([], function () {
    
    var getBookmarks = async function (callback, token) {
        var response = await fetch ("api/bookmarks", {
            method: "GET",
            headers: {
                //"Content-Type": 'application/json;charset=utf-8',
                "Authorization": "Bearer " + token
            }
        });
        var data = await response.json();
        callback(data);
    };
    
    var createBookmark = async function (callback, postId, note, token) {
          var response = await fetch ("api/bookmarks", {
              method: "POST",
              headers: {
                "Content-Type": 'application/json;charset=utf-8',
                "Authorization": "Bearer " + token
              },
              body: JSON.stringify({
                  postId,
                  note
              })
          });
          var data = await response.json();
          callback(data);
    };
    
    var updateBookmark = async function (callback, bookmarkId, note, token) {
        var response = await fetch ("api/bookmarks", {
            method: "PUT",
            headers: {
                "Content-Type": 'application/json;charset=utf-8',
                "Authorization": "Bearer " + token
            },
            body: JSON.stringify({
                bookmarkId,
                note
            })
        });
        var data = await response.json();
        callback(data);
    };

    var deleteBookmark = async function (callback, bookmarkId, token) {
        var response = await fetch ("api/bookmarks/" + bookmarkId, {
            method: "DELETE",
            headers: {
                //"Content-Type": 'application/json;charset=utf-8',
                "Authorization": "Bearer " + token
            }
        });
        var data = await response.json();
        callback(data);
    };
    
    return {
        getBookmarks,
        createBookmark,
        updateBookmark,
        deleteBookmark
    }
});