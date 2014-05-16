(function () {

    var app = angular.module("common", []);

    app.config(function($provide) {
        $provide.decorator("$log", function($delegate, errors) {
            var error = $delegate.error;
            $delegate.error = function () {
                errors.addError(arguments[0]);
                error.apply(this, arguments);
            };
            return $delegate;
        });
    });

    app.factory("errors", function() {

        var errors = [];

        var addError = function(error) {
            errors.push(error);
        };

        var catchAll = function(description) {
            return function(error, reason) {
                addError(description);
            };
        };
        
        return {
            catchAll: catchAll,
            addError: addError,
            currentErrors: errors
        };
    });

    app.directive("errorList", function(errors) {
        return {
            restrict: "EA",
            template: "<div ng-repeat='error in errors'>{{error}}</div>",
            link: function (scope) {
                console.log("link");
                scope.errors = errors.currentErrors;
            }
        };
    });

    app.factory("webapi", function () {

        var defaultActions = {
               "get": { method: "GET" },
               "save": { method: "POST" },
               "update": { method: "PUT "},
               "query": { method: "GET", isArray: true },
               "remove": { method: "DELETE" },
               "delete": { method: "DELETE" }
           };

        return {
            machines: { url: "api/vms/:id", actions: defaultActions }
        };
    });

}());