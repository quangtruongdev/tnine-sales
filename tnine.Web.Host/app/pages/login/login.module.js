(function() {
    'use strict';

    angular.module('tnine.auth', [
        'tnine.services'
    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: '/app/pages/login/login.component.html',
                controller: 'loginController',
                controllerAs: 'vm'
            });
        $urlRouterProvider.otherwise('/login');
    }

})();