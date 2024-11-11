(function (app) {
    app.controller('shopController', shopController)
        .component('shop', {
            templateUrl: '/app/admin/shop/shop.component.html',
            controller: 'shopController',
            controllerAs: 'vm'
        });

    shopController.$inject = ['serviceProxies'];

    function shopController(serviceProxies) {
        var vm = this;
        vm.shops = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" },
            { headerName: "Address", field: "Address" },
            { headerName: "PhoneNumber", field: "PhoneNumber" }
        ];

        vm.getShops = function () {
            serviceProxies.shopService.getAll().then(function (response) {
                vm.shops = response;
            }).catch(function (error) {
                console.error('Error fetching shops:', error);
            });
        };

        vm.reloads = function () {
            vm.getShops();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-shop-modal')).controller('createOrEditShopModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-shop-modal')).controller('createOrEditShopModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.shopService.delete(vm.selectedId).then(function () {
                    vm.getShops();
                }).catch(function (error) {
                    console.error('Error deleting shop:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getShops();
    }

})(angular.module('app.pages.shop'));