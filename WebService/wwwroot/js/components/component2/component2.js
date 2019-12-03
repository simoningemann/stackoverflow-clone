define(['knockout', 'postman'], function (ko, postman) {
    return function (params) {
        var person = ko.observable();

        postman.subscribe("selectperson", function(data) {
            person(data);
        });


        return {
            person
        };
    };
});