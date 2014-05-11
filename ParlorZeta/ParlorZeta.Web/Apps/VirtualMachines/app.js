(function() {
    var app = angular.module("virtualMachines", ["common", "ngAnimate", "ngResource"]);

    app.factory("machines", function($resource, webapi) {
        return $resource(webapi.machines.url, null, webapi.machines.defaultActions);
    });

    app.controller("vmController", function($scope, machines) {

        var setMachines = function (data) {
            $scope.machines = data;
        };

        var setError = function(reason) {
            $scope.error = reason;
        };

        machines.query(setMachines);
    });

}());

