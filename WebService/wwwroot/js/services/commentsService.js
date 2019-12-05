define([], function () {
    
    var getComments = async function(callback, postIds) {
        var url = createGetCommentsUrl(postIds);
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };
    
    function createGetCommentsUrl (postIds)  {
        var url = "api/comments/?";
        for(var postid of postIds) {
            url = url + "postIds=" + postid + "&";
        }
        url = url.slice(0, url.length-1);
        return url;
    }
    
    return {
        getComments
    };
});