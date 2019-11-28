require.config({
    //baseUrl: "",
    paths: {
        // add if needed jquery: "lib/jquery/dist/jquery",
        knockout: "lib/knockout/build/output/knockout-latest.debug"
    }
});

require(["knockout", "searchVm"], function(ko, svm){
    console.log("hello from main");
});
