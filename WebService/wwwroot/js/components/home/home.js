define(["knockout", "postmanager", "questionService"], function(ko, pm, qs) {
    
    console.log("hello from home");
    var input = ko.observable("");
    var results = ko.observable({});
    var page = ko.observable(1);
    var size = ko.observable(10);
    var sizes = ko.observableArray([10, 20, 30, 40, 50]);

    var search = async function () {
        var keywords = input().toLowerCase().split(" ");
        await qs.searchQuestions(function(data) {
            results(data);
        }, page(), size(), keywords);
        console.log(results());
    };

    page.subscribe(search);

    var pages = function () {
        var pages = [];
        for(var i = 1; i <= results().totalPages; i++)
            pages.push(i);
        return pages;
    };
    
    var showWordCloud = function (postId) {
        pm.publish("changePostId", postId);
        pm.publish("changeComponent", "wordcloud");
    };
    
    var isPageActive = function (somePage) {
        if (somePage === page())
            return "active";
        return "";
    };
    
    return function () {        
        return {
            input,
            search,
            results,
            page,
            size,
            sizes,
            pages,
            showWordCloud,
            isPageActive
        }
    }
});