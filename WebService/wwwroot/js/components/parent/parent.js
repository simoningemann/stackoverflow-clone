define(['knockout', 'dataService'], function(ko, ds) {
    return function (params) {
        var persons = ko.observableArray([]);
        var selectedPerson = ko.observable();
        var selectPerson = function(data) {
            selectedPerson(data);
        };

        ds.getNames(persons);

        return {
            persons,
            selectedPerson,
            selectPerson
        };
    };
});