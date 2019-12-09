define([], function () {

    var login = async function (callback, email, password) {
        var response = await fetch("api/profiles/login", {
            method: "POST",
            headers: {
                "Content-Type": 'application/json;charset=utf-8'
            },
            body: JSON.stringify({
                email,
                password
            })
        });
        var data = await response.json();
        callback(data, response);
        
    };

    var signup = async function (callback, email, password) {
        var response = await fetch("api/profiles", {
            method: "POST",
            headers: {
                "Content-Type": 'application/json;charset=utf-8'
            },
            body: JSON.stringify({
                email,
                password
            })
        });
        var data = await response.json();
        callback(data, response);
    };
    var deletion = async function (callback, password, token) {
        console.log(token);
        var response = await fetch("api/profiles/delete", {
            method: "POST",
            headers: {
                "Content-Type": 'application/json;charset=utf-8',
                "Authorization": "Bearer " + token
            },
            body: JSON.stringify({
                password
            })
        });
        var data;
        if(response.status !== 401)
            data = await response.json();
        else
            data = {error: "Unauthorized"};
        callback(data, response);
    };
    

    return {
        login,
        signup,
        deletion
    }
});