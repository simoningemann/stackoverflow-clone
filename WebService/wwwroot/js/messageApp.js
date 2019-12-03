define(["knockout"], function (ko) {
    var menuElements = [
        {
            name: "Home",
            component: "component1"
        },
        {
            name: "Cloud",
            component: "cloud"
        }
    ];
    var loginElem = { name: "Login", component: "component2"};

    var currentComponent = ko.observable("component1");

    var changeContent = function(menu) {
        currentComponent(menu.component);
    };
    
    return {
        currentComponent,
        changeContent,
        menuElements,
        loginElem
    };
});