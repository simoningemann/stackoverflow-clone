define(["knockout", "postmanager"], function(ko, pm) {

    console.log("hello from wordcloud");
    
    var postId = ko.observable(19);
    
    pm.subscribe ("changePostId", postId);
    
    return function () {
        return {
            postId
        }
    }
});