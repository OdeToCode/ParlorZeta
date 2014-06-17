(function () {

    var app = angular.module("virtualMachines", ["common", "ngAnimate"]);

    app.controller("vmController", function ($scope, webapi, errors) {

        var onMachines = function(machines) {
            $scope.machines = machines;
        };

        webapi.all("vm").getList()
            .then(onMachines)
            .catch(errors.catchAll("Could not retrieve virtual machines"));
    });

}());

