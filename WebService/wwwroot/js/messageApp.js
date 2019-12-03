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

    var currentComponent = ko.observable("cloud");

    var changeContent = function(menu) {
        currentComponent(menu.component);
    };
    
    return {
        currentComponent,
        changeContent,
        menuElements
    };
});