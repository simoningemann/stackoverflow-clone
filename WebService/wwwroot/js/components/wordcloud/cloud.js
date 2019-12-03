define(['jquery', 'dataService', 'jqcloud'], function ($, ds) {

    ds.getWords(function(words) {
        $('#cloud').jQCloud(words,
            {
                width: 500
            });
    });

    

    return function(params) {
        return {
        };
    };
});