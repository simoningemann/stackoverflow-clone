
require.config({
    baseUrl: "js",
    paths: {
        jquery: "../lib/jquery/dist/jquery",
        knockout: "../lib/knockout/build/output/knockout-latest.debug",
        text: "../lib/requirejs-text/text",
        jqcloud: "../lib/jqcloud2/dist/jqcloud",
        dataService: "services/dataService",
        postman: "services/postman"
    },
    shim: {
        jqcloud: ["jquery"]
    }
});

require(["knockout"], function(ko) {
    ko.components.register('cloud', {
        viewModel: { require: "components/wordcloud/cloud" },
        template: { require: "text!components/wordcloud/cloud.html" }
    });
    ko.components.register('parent', {
        viewModel: { require: "components/parent/parent" },
        template: { require: "text!components/parent/parent.html" }
    });
    ko.components.register('child', {
        viewModel: { require: "components/child/child" },
        template: { require: "text!components/child/child.html" }
    });
    ko.components.register('component1', {
        viewModel: { require: "components/component1/component1" },
        template: { require: "text!components/component1/component1.html" }
    });
    ko.components.register('component2', {
        viewModel: { require: "components/component2/component2" },
        template: { require: "text!components/component2/component2.html" }
    });
});


require(["knockout", "messageApp"], function(ko, app) {
    ko.applyBindings(app);
});