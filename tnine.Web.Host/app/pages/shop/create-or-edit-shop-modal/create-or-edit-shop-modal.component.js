(function (app) {
    app.controller('CreateOrEditShopModalController', CreateOrEditShopModalController)
        .component('createOrEditShopModal', {
            templateUrl: '/app/pages/shop/create-or-edit-shop-modal/create-or-edit-shop-modal.component.html',
            controller: 'CreateOrEditShopModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditShopModalController.$inject = ['serviceProxies'];

    function CreateOrEditShopModalController(serviceProxies) {
        var vm = this;
        vm.shop = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.shop = {};
            } else {
                serviceProxies.shopService.getShopForEdit(id).then(function (response) {
                    vm.shop = response.Shop;
                }).catch(function (error) {
                    console.error('Error fetching shop:', error);
                });
            }
            $('#createOrEditShopModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.shopService.createOrEdit(vm.shop).then(function () {
                vm.close();
                location.reload();
            }).catch(function (error) {
                console.error('Error saving shop:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditShopModal').modal('hide');
        };
    }

})(angular.module('app.pages.shop'));