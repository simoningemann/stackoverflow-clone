define([], function() {
    
    var getUser = async function (callback, userId) {
        var response = await fetch("api/users/" + userId);
        var data = await response.json();
        callback(data);
    };
    
    var getUsers = async function (callback, userIds) {
        var url = createGetUsersUrl(userIds);
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };
    
    function createGetUsersUrl (userIds) {
        var url = "api/users?";
        for(var userId of userIds)
            url = url + "userIds" + userId + "&";
        url = url.slice(0, url.length-1);
        return url;
    }
    
    return {
        getUser,
        getUsers
    };
});