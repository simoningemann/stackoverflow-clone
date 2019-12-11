define(["knockout", "postmanager", "profileService"], function(ko, pm, ps) {
    
    console.log("hello from profile");
    
    var email = ko.observable("");
    var password = ko.observable("");
    var profile = ko.observable({});

    var login = async function () {
        await ps.login(function (data, response) {
            if(response.ok) {
                profile(data);
                alert ("Login successful");
                pm.publish("login", profile());
            }
            else
                alert(data.error);
        }, email(), password());
        console.log(profile());
    };

    var signup = async function () {
        await ps.signup(function (data, response) {
            if(response.ok) {
                alert ("Created profile with email " + data.email);
            }
            else 
                alert(data.error);
        }, email(), password());
    };

    var deletion = async function () {
        await ps.deletion(function (data, response) {
            if(response.ok) {
                alert ("Deleted profile with email " + data.email);
                profile({});
                pm.publish("login", profile());
            }
            else
                alert(data.error);
        }, password(), profile().token);
    };
    
    return function () {
        return {
            email,
            password,
            profile,
            login,
            signup,
            deletion
        }
    }
});