define(["knockout", "postmanager", "profileService"], function(ko, pm, ps) {
    
    console.log("hello from profile");
    
    var email = ko.observable("");
    var password = ko.observable("");
    var profile = ko.observable({});
    
    var login = function () {
        console.log("login " + email() + password());
    };

    var signup = async function () {
        console.log("signup " + email() + password());
        await ps.signup(function (data) {
            profile(data);
        }, email(), password());
        console.log(profile());
    };
    
    return function () {
        return {
            email,
            password,
            profile,
            login,
            signup
        }
    }
});