require.config({
    
    paths: {
        jquery: "lib/jquery/dist/jquery",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        text: "lib/requirejs-text/text",
        jqcloud: "lib/jqcloud2/dist/jqcloud",
        bootstrap: "lib/bootstrap/dist/js/bootstrap.bundle",
        answerService: "js/services/answerService",
        commentService: "js/services/commentService",
        postService: "js/services/postService",
        questionService: "js/services/questionService",
        userService: "js/services/userService",
        profileService: "js/services/profileService",
        bookmarkService: "js/services/bookmarkService",
        // more services later
        postmanager: "js/services/postmanager",
        app: "js/app"
    },
    
    shim: {
        jqcloud: ["jquery"],
        bootstrap: ["jquery"]
    }
    
});

require(["knockout"], function(ko) {
    
    console.log("hello from component registration");
    
    ko.components.register("home", {
        viewModel: { require: "js/components/home/home" },
        template: { require: "text!js/components/home/home.html" }
    });
    
    ko.components.register("navbar", {
        viewModel: { require: "js/components/navbar/navbar" },
        template: { require: "text!js/components/navbar/navbar.html" }
    });

    ko.components.register("profile", {
        viewModel: { require: "js/components/login/login" },
        template: { require: "text!js/components/login/login.html" }
    });

    ko.components.register("wordcloud", {
        viewModel: { require: "js/components/wordcloud/wordcloud" },
        template: { require: "text!js/components/wordcloud/wordcloud.html" }
    });

    ko.components.register("post", {
        viewModel: { require: "js/components/post/post" },
        template: { require: "text!js/components/post/post.html" }
    });

    ko.components.register("bookmark", {
        viewModel: { require: "js/components/bookmark/bookmark" },
        template: { require: "text!js/components/bookmark/bookmark.html" }
    });
    
});

require(["knockout", "jquery", "bootstrap", "postmanager", "app"], function(ko, jq, bs, pm, app){
    
    console.log("hello from main");

    ko.applyBindings(app);

    // preload components so that they may subscribe
    var componentsForPreload = ["login", "wordcloud", "bookmark", "profile", "post", "home"]; // end with the starting component
    for (var component of componentsForPreload)
        pm.publish("changeComponent", component);
    
});
