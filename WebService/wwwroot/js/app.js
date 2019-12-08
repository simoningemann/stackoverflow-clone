define(["knockout", "postmanager"], function(ko, pm) {
    
    console.log("Hello from app");
    
    const navbar = ko.observable("navbar");

    var currentComponent = ko.observable("home");
    
    pm.subscribe("changeComponent", currentComponent);
    
    
    return {
        navbar,
        currentComponent
    };
});