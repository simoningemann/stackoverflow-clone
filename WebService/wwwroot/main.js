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
        userService: "js/services/userService",
        // more services later
        app: "js/stackOverflowApp"
    },
    shim: {
        jqcloud: ["jquery"]
    }
});

require(["knockout"], function(ko) {
    ko.components.register("mainComponent", {
        viewModel: { require: "js/components/mainComponent/mainComponent" },
        template: { require: "text!js/components/mainComponent/mainComponent.html" }
    });
});


require(["knockout", "postmanager", "app"], function(ko, pm, app){
    console.log("hello from main");
});
