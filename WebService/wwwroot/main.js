require.config({
    //baseUrl: "",
    paths: {
        // add if needed jquery: "lib/jquery/dist/jquery",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        text: "lib/requirejs-text/text"
    }
});

require(["knockout", "text", "searchVm"], function(ko, tx, svm){
    console.log("hello from main");
});
