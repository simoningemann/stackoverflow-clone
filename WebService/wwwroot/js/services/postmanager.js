define([], function() {
    
    console.log("hello from postmanager");
    
    var subscribers = [];
    
    // call when wanting to know about changes
    var subscribe = function(event, callback) {
        var subscriber = {event, callback};

        subscribers.push(subscriber);

        return function() { // ??
            subscribers = subscribers.filter(x => x !== subscriber);
        };
    };
    
    // call when changes happen
    var publish = function(event, data) {
       for(var subscriber of subscribers)
           if(subscriber.event === event)
               subscriber.callback(data);
    };
    
    return {
        subscribe,
        publish
    }
});