(function () {
    var app = angular.module("virtualMachines", ["common", "ngAnimate"]);

    app.controller("vmController", function ($scope, webapi) {

        webapi.all("vm").getList()
            .then(function (machines) {
                $scope.machines = machines;
            });
    });

}());

