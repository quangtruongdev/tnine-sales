(function () {
    angular.module('tnine.product', []).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('product', {
                url: '/product',
                parent: 'base',
                templateUrl: '/app/pages/product/product.component.html',
                controller: 'productController',
            });
        $urlRouterProvider.otherwise('/product');
    }
})();