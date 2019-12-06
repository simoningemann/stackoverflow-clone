define(["knockout", "postmanager"], function (ko, pm) {
    return function () {
        console.log("Hello from mainComponent");
        
        pm.publish("changeComponent", "signupComponent");

        return {
        };
    }
});