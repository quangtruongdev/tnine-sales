/// <reference path="../../../wwwroot/lib/angular/angular.js" />

(function () {
    var home = angular.module('tnine.home', []);

    home.controller('homeController', ['$scope', function ($scope) {
        $scope.message = "BDES"; // Parent message
    }]);
})();