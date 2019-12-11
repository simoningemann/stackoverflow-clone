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
    
    var isLoginPromptVisible = function () {
        if(profile().token === undefined)
            return "";
        return "d-none";
    };
    
    return function () {
        
        getBookmarks();
        
        return {
            getBookmarks,
            displayLoginPrompt: isLoginPromptVisible
        }
    }
});