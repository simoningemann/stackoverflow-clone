define(["knockout", "postmanager"], function(ko, pm) {
    return function () {
        
        console.log("hello from navbar");
        
        var options = ["home", "login"];
        
        var changeComponent = function (component) {
            pm.publish("changeComponent", component);
        };
        
        return {
            options,
            changeComponent
        }
    }
});