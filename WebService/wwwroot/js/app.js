define(["knockout"], function (ko) {
    var currentComponent = ko.observable("parent");
    var currentParams = ko.observable({});
    var changeContent = () => {
        //if (currentComponent() === "page1") {
        //    currentParams({ name: 'Ellen' });
        //    currentComponent("page2");
        //} else {
        //    currentParams({});
        //    currentComponent("page1");
        //}
    };


    return {
        currentComponent,
        currentParams,
        changeContent

    };
});