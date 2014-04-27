(function() {
    var app = angular.module("virtualMachines", ["ngResource"]);

    app.factory("vm", function($resource) {
        return $resource("/api/vm/:id", null,
            {                
                
            });
    });

    app.controller("vmController", function($scope, vm) {
        $scope.machines = vm.query();
    });

}());