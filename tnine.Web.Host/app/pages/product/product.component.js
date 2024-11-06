(function (app) {
    app.controller('productController', productController)
        .component('product', {
            templateUrl: '/app/pages/product/product.component.html',
            controller: 'productController',
            controllerAs: 'vm'
        });

    productController.$inject = ['serviceProxies', '$state', '$element'];

    function productController(serviceProxies, $state, $element) {
        var vm = this;
        vm.products = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" },
            { headerName: "Price", field: "Price" },
            { headerName: "Category", field: "CategoryName"},
            { headerName: "Description", field: "Description" }
        ];

        vm.getProducts = function () {
            serviceProxies.productService.getAll().then(function (response) {
                vm.products = response.Results;
            }).catch(function (error) {
                console.error('Error fetching products:', error);
            });
        };

        vm.reload = function () {
            vm.getProducts();
        };
        
        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-product-modal')).controller('createOrEditProductModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-product-modal')).controller('createOrEditProductModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.productService.delete(vm.selectedId).then(function () {
                    vm.getProducts();
                }).catch(function (error) {
                    console.error('Error deleting product:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getProducts();
    }

})(angular.module('app.pages.product'));