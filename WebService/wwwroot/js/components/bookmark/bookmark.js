define(["knockout", "postmanager", "bookmarkService"], function(ko, pm, bs) {
    
    console.log("hello from bookmark");
    var profile = ko.observable({});
    pm.subscribe("login", profile);
    var bookmarks = ko.observable({});
    
    var getBookmarks = async function () {
        await bs.getBookmarks(function(data) {
            bookmarks(data);
        }, profile().token);
        console.log(bookmarks());
    };

    var updateBookmark = async function (bookmarkId, note) {
        await bs.updateBookmark(function (data) {
            alert("Updated note of bookmark with id " + data.bookmarkId);
        }, bookmarkId, note, profile().token);
    };
    
    var deleteBookmark = async function (bookmarkId) {
        await bs.deleteBookmark(function (data) {
            alert("Deleted bookmark with id " + data.bookmarkId);
        }, bookmarkId, profile().token);
        await getBookmarks();
    };
    
    var goToPost = function (postId) {
        pm.publish("changePostId", postId);  
        pm.publish("changeComponent", "post")
    };
    
    var isLoginPromptVisible = function () {
        if(profile().token === undefined)
            return "";
        return "d-none";
    };
    
    return function () {
        
        getBookmarks();
        window.scrollTo(0, 0);
        
        return {
            bookmarks,
            getBookmarks,
            isLoginPromptVisible,
            goToPost,
            deleteBookmark,
            updateBookmark
        }
    }
});