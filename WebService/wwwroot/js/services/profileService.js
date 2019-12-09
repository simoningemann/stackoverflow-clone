define([], function () {
    
    var login = async function () {
        var response = fetch();
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
        callback(data);
    };

    return {
        login,
        signup
    }
});