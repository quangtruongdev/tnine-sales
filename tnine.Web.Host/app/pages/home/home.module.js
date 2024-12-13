/// <reference path="../../../wwwroot/lib/angular/angular.js" />

(function () {
    var home = angular.module('tnine.home', []);

    home.controller('homeController', homeController)
        .component('home', {
            templateUrl: '/app/pages/home/home.component.html',
            controller: homeController,
            controllerAs: 'vm'
        });
})();