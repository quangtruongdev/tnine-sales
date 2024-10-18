/// <reference path="../../../wwwroot/lib/angular/angular.js" />

(function () {
    var home = angular.module('tnine.home', [
        //'tnine.common'
    ]);

    home.controller('homeController', ['$scope', function ($scope) {
        $scope.message = "ABC";
    }]);
})();