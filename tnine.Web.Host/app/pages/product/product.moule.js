(function () {
    angular.module('tnine.product', [
        'tnine.common',
        'tnine.ui'
    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('product', {
                url: '/product',
                parent: 'base',
                templateUrl: '/app/pages/product/product.component.html',
                controller: 'productController',
                //resolve: {
                //    loadModule: ['$ocLazyLoad', function ($ocLazyLoad) {
                //        return $ocLazyLoad.load([
                //            '/app/pages/product/product.module.js',
                //            '/app/pages/product/product.controller.js',
                //            '/app/pages/product/product.service.js'
                //        ]);
                //    }]
                //}
            });
        $urlRouterProvider.otherwise('/product');
    }
})();