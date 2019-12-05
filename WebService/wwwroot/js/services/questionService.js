define([], function () {

    var searchQuestions = async function(
        callback, pageNum, pageSize, keywords) {
        var url = createSearchQuestionsUrl(pageNum, pageSize, keywords);
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };
    
    var getQuestion = async function(callback, questionId) {
        var response = await fetch("api/questions/" + questionId);
        var data = await response.json();
        callback(data);
    };
    
    var getLinks = async function(callback, questionId) {
        var response = await fetch("api/questions/links/" + questionId);
        var data = await response.json();
        callback(data);
    };
    
    function createSearchQuestionsUrl (pageNum, pageSize, keywords) {
        var url = "api/questions?pageNum=" + pageNum + "&pageSize=" + pageSize + "&";
        for(var keyword of keywords) {
            url = url + "keywords=" + keyword + "&";
        }
        url = url.slice(0, url.length-1);
        return url;
    }
    
    return {
        searchQuestions,
        getQuestion,
        getLinks
    };
});