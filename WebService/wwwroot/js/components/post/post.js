define(["knockout", "postmanager", "questionService", "postService", "answerService", 
    "commentService", "userService", "bookmarkService"],
    function(ko, pm, qs, ps, as, cs, us, bs) {
    
    console.log("hello from post");
    var postId = ko.observable(19);
    pm.subscribe("changePostId", postId);
    var question = ko.observable({});
    var post = ko.observable({});
    var answers = ko.observable({});
    var users = ko.observable({});
    var profile = ko.observable({});
    var bookmarks = ko.observable({bookmarks: []});
    pm.subscribe("bookmarks", bookmarks);
    pm.subscribe("login", profile);
    
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
    
    var createBookmark = async function () {
        await bs.createBookmark(function (data) {
            alert("Created bookmark with id " + data.bookmarkId); 
        }, postId(), "", profile().token);
        await getBookmarks();
    };

    var showWordCloud = function () {
        pm.publish("changeComponent", "wordcloud");
    };

    var back = function () {
        pm.publish("changeComponent", "home");
    };
    
    var isBookmarkVisible = function () {
        if(profile().token === undefined)
            return "d-none";
        else {
            var postIds = [];
            bookmarks().bookmarks.forEach(function (value) {postIds.push(value.postId)});
            console.log(postIds);
            if(postIds.includes(postId())) return "d-none";
        }
        return "";
    };
    
    var getBookmarks = async function () {
        await bs.getBookmarks(function (data) {
            if (data.bookmarks !== undefined)
                bookmarks(data)
        }, profile().token);
    };
    
    return function () {
        
        getQuestion();
        getPost();
        getAnswers();
        getBookmarks();
        window.scrollTo(0, 0);

        return {
            question,
            post,
            answers,
            back,
            showWordCloud,
            createBookmark,
            isBookmarkVisible
        }
    }
});