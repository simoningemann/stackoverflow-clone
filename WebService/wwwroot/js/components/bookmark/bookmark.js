define(["knockout", "postmanager", "bookmarkService", "questionService"], function(ko, pm, bs, qs) {
    
    console.log("hello from bookmark");
    var profile = ko.observable({});
    pm.subscribe("login", profile);
    var bookmarks = ko.observable({bookmarks: []});
    var questions = ko.observable({questions: []});
    
    var getBookmarks = async function () {
        await bs.getBookmarks(function(data) {
            if(data.bookmarks !== undefined)
                bookmarks(data);
        }, profile().token);
        console.log(bookmarks());
        var postIds = [];
        for (var bookmark of bookmarks().bookmarks)
            postIds.push(bookmark.postId);
        await getQuestions(postIds);
    };
    
    var getQuestions = async function (postIds) {
        await qs.getQuestions(function (data) {
            if(data.questions !== undefined)
                questions(data);
        }, postIds);
        console.log(questions());
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
    
    var getTitle = function (postId) {
        for(var question of questions().questions)
            if(question.postId === postId)
                return question.title;
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
            updateBookmark,
            getTitle
        }
    }
});