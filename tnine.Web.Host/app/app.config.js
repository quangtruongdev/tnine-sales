(function (app) {
    app.config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                abstract: true,
                templateUrl: '/app/shared/layout/layout.component.html',
            })
            .state('login', {
                url: '/login',
                templateUrl: '/app/pages/login/login.component.html',
                controller: 'loginController',
            })
            .state('home', {
                url: '/home',
                parent: 'base',
                templateUrl: '/app/pages/home/home.component.html',
                controller: 'homeController',
            });
        $urlRouterProvider.otherwise('/home');
    }
})(angular.module('tnine'));