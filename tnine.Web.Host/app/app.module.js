/// <reference path="../wwwroot/lib/angular/angular.js" />

(function () {
    angular.module('tnine', [
        'ui.router',
        'tnine.common',
        'tnine.todo'
    ]).config(config)
      .config(configAuth);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                abstract: true,
                templateUrl: 'app/shared/layout.html',
            })
            .state('login', {
                url: '/login',
                templateUrl: 'app/pages/login/login.html',
                controller: 'loginController',
            })
            .state('home', {
                url: '/home',
                parent: 'base',
                templateUrl: 'app/pages/home/home.html',
                controller: 'homeController',
            });
        $urlRouterProvider.otherwise('/login');
    }

    function configAuth($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    //config.headers = config.headers || {};
                    //if (localStorage.getItem('token')) {
                    //    config.headers.Authorization = 'Bearer ' + localStorage.getItem('token');
                    //}
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status === 401) {
                        $location.path('/login');
                    }
                    return response || $q.when(response);
                },
                responseError: function (rejection) {
                    if (rejection.status === 401) {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();