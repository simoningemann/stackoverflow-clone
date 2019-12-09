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
        await ps.signup(function (data, response) {
            if(response.ok) {
                profile(data);
                alert ("Created profile with email " + profile().email);
            }
            else 
                alert(data.error);
        }, email(), password());
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