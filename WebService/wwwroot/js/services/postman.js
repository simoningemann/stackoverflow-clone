define([], function() {
    var subscribers = [];

    var subscribe = function(event, callback) {
        var subscriber = { event, callback };
        subscribers.push(subscriber);

        return function() {
            subscribers =
                subscribers.filter(x => x !== subscriber);
        };
    };

    var publish = function(event, data) {
        subscribers.forEach(x => {
            if (x.event === event) {
                x.callback(data);
            }
        });
    };


    return {
        subscribe,
        publish
    };
});