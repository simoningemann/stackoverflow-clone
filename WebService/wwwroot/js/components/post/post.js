define(["knockout", "postmanager", "questionService", "postService", "answerService", "commentService"],
    function(ko, pm, qs, ps, as, cs) {
    
    console.log("hello from post");
    var postId = ko.observable(19);
    pm.subscribe("changePostId", postId);
    var question = ko.observable({});
    var post = ko.observable({});
    var answers = ko.observable({});
    
    var getQuestion = async function () {
        await qs.getQuestion(function (data) {
            question(data);
        }, postId());
        console.log(question());
    };

    var getAnswers = async function () {
        var postIds = [];
        await as.getAnswers(function (data) {
            for(var answer in data.answers)
                postIds.push(data.answers[answer].postId);
        }, postId());
        await ps.getPosts(function (data) {
            answers(data);
        }, postIds);
        console.log(answers());
    };

    var getPost = async function () {
        await ps.getPost(function (data) {
            post(data);
        }, postId());
        console.log(post());
    };

    var showWordCloud = function () {
    pm.publish("changeComponent", "wordcloud");
    };

    var back = function () {
        pm.publish("changeComponent", "home");
    };
    
    return function () {
        
        getQuestion();
        getPost();
        getAnswers();
        
        return {
            question,
            post,
            answers,
            back,
            showWordCloud
        }
    }
});