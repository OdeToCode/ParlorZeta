(function() {
    var app = angular.module("virtualMachines", ["common", "ngAnimate", "ngResource"]);

    app.factory("machines", function($resource, webapi) {
        return $resource(webapi.machines.url, null, webapi.machines.defaultActions);
    });

    app.controller("vmController", function($scope, machines, errors) {

        $scope.machines = machines.query(angular.noop, errors.catchAll("Could not fetch machine list"));
    });

}());

