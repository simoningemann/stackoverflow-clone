require.config({
    // baseUrl: "", optional
    paths: {
        jquery: "lib/jquery/dist/jquery",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        text: "lib/requirejs-text/text",
        jqcloud: "lib/jqcloud2/dist/jqcloud",
        answerService: "js/services/answerService",
        commentService: "js/services/commentService",
        postService: "js/services/postService",
        questionService: "js/services/questionService",
        userService: "js/services/userService"
        // more services later
        // app: ""
    },
    shim: {
        jqcloud: ["jquery"]
    }
});

require(["knockout"], function(ko) {
    /* load component example
    ko.components.register("name", {
        viewModel: { require: "path/to/jsfile" },
        template: { require: "text!path/to/file.html" }
    });
    */
});


require(["knockout"], function(ko){
    console.log("hello from main");
});
