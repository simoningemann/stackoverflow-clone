define([], function () {
    
    var getAnswers = async function(callback, questionId) {
          var response = await fetch ("api/answers/" + questionId);
          var data = await response.json();
          callback(data);
    };
    
    return {
        getAnswers   
    };
});