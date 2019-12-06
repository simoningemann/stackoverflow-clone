define(["knockout", "postmanager"], function(ko, pm) {
    
    console.log("Hello from app");
    
    var currentComponent = ko.observable("mainComponent");
    
    pm.subscribe("changeComponent", currentComponent);
    
    return {
        currentComponent
    };
});