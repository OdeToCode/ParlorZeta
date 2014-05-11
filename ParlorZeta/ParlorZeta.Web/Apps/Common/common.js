(function () {

    var app = angular.module("common", []);

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
            machines: { url: "api/vm/:id", actions: defaultActions }
        };
    });

}());