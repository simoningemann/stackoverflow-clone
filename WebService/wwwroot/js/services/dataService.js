define([], function() {
    
    var getNames = async function(callback) {
        const response = await fetch("api/names");
        const data = await response.json();
        callback(data);
    };

    var getWords = async function(callback) {
        var response = await fetch("api/words");
        var data = await response.json();
        callback(data);
    };


    return {
        getNames,
        getWords
    };
});