define([], function() {
    var getPost = async function(callback, postId) {
        var response = await fetch("api/posts/" + postId);
        var data = await response.json();
        callback(data);
    };
    
    var getPosts = async function(callback, postIds) {
        var url = createGetPostsUrl(postIds);
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };
    
    var getWordCloud = async function (callback, postId) {
        var response = await fetch("api/posts/wordcloud/" + postId);
        var data = await response.json();
        callback(data);
    };
    
    function createGetPostsUrl (postIds) {
        var url = "api/posts/?";
        for (var postId of postIds)
            url = url + "postIds=" + postId + "&";
        url = url.slice(0, url.length-1);
        return url;
    }
    
    return {
        getPost,
        getPosts,
        getWordCloud
    };
});