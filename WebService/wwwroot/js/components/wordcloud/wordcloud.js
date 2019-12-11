define(["jquery", "knockout", "postmanager", "postService", "jqcloud"], function($, ko, pm, ps) {

    console.log("hello from wordcloud");
    
    var postId = ko.observable(19);
    var results = ko.observable({});
    
    
    var getWordCloud = async function () {
        await ps.getWordCloud(function (data) {
            results(data);
        }, postId());
        console.log(results());
        $('#cloud').jQCloud(results().words, {
            autoResize: true,
        });
    };
    
    var back = function () {
        pm.publish("changeComponent", "post");  
    };
    
    pm.subscribe ("changePostId", postId);
    
    return function () {
        
        getWordCloud();
        window.scrollTo(0, 0);
        
        return {
            postId,
            results,
            back
        }
    }
});