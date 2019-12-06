define(["knockout", "postmanager", "questionService"], function(ko, pm, qs) {
    return function () {
        console.log("hello from home");
        var input = ko.observable("");
        var results = ko.observable({});
        
        var search = async function () {
            var keywords = input().split(" ");
            await qs.searchQuestions(function(data) {
                results(data);
            }, 1, 10, keywords);
            console.log(results());
        };
        
        return {
            input,
            search,
            results
        }
    }
});