(function (app) {
    app.config(config)
        .config(configAuth);

    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ROUTES'];

    function config($stateProvider, $urlRouterProvider, $locationProvider, ROUTES) {
        //$locationProvider.html5Mode(true);

        ROUTES.forEach(function (route) {
            var stateConfig = {
                url: route.url,
                templateUrl: route.templateUrl,
                controller: route.controller,
                resolve: {
                    load: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load(route.files);
                    }],
                    auth: ['$q', '$location', 'authService', function ($q, $location, authService) {
                        if (route.requiresAuth && !authService.isAuthenticated()) {
                            $location.path('/login');
                            return $q.reject('Not Authorized');
                        }
                    }]
                }
            };

            if (route.name !== 'login') {
                stateConfig.parent = 'base';
            }

            $stateProvider.state(route.name, stateConfig);
        });

        $stateProvider.state('base', {
            abstract: true,
            templateUrl: '/app/shared/layout/layout.component.html',
            resolve: {
                load: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        '/app/shared/layout/layout.module.js',
                        '/app/shared/layout/sidebar/sidebar.component.js',
                        '/app/shared/layout/sidebar/app-menu.js',
                        '/app/shared/layout/sidebar/app-menu-item.js',
                        '/app/shared/layout/topbar/topbar.component.js',
                    ]);
                }]
            }
        });

        $urlRouterProvider.otherwise('/home');
    }

    function configAuth($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
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
            }
        });
    }
})(angular.module('root'));
