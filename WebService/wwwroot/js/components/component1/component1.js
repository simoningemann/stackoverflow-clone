define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var persons = ko.observableArray([]);
        //var selectedPerson = ko.observable();
        var selectPerson = function (data) {
            postman.publish("selectperson", data);
            //selectedPerson(data);
        };

        ds.getNames(persons);

        return {
            persons,
            //selectedPerson,
            selectPerson
        };
    };
});