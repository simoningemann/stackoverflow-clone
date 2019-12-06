require.config({
    
    paths: {
        jquery: "lib/jquery/dist/jquery",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        text: "lib/requirejs-text/text",
        jqcloud: "lib/jqcloud2/dist/jqcloud",
        answerService: "js/services/answerService",
        commentService: "js/services/commentService",
        postService: "js/services/postService",
        questionService: "js/services/questionService",
        userService: "js/services/userService",
        // more services later
        postmanager: "js/services/postmanager",
        app: "js/app"
    },
    
    shim: {
        jqcloud: ["jquery"]
    }
    
});

require(["knockout"], function(ko) {
    
    ko.components.register("home", {
        viewModel: { require: "js/components/home/home" },
        template: { require: "text!js/components/home/home.html" }
    });
    
    ko.components.register("navbar", {
        viewModel: { require: "js/components/navbar/navbar" },
        template: { require: "text!js/components/navbar/navbar.html" }
    });
    
});

require(["knockout", "postmanager", "app"], function(ko, pm, app){
    
    console.log("hello from main");
    ko.applyBindings(app);
    
});
