(function (app) {
    app.controller('CreateOrEditOrderModalController', CreateOrOrderModalController)
        .component('createOrEditOrderModal', {
            templateUrl: '/app/admin/order/create-or-edit-order-modal/create-or-edit-order-modal.component.html',
            controller: 'CreateOrEditorderModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditOrderModalController.$inject = ['serviceProxies'];

    function CreateOrEditOrderModalController(serviceProxies, $element) {
        var vm = this;
        vm.order = {};
        vm.saving = false;
        vm.show = function (id) {
            if (!id) {
                vm.order = {};
            } else {
                serviceProxies.orderService.getOrderForEdit(id).then(function (response) {
                    vm.order = response.Order;
                }).catch(function (error) {
                    console.error('Error fetching order:', error);
                });
            }



            $('#createOrEditOrderModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.orderService.createOrEdit(vm.order).then(function () {
                vm.close();
                location.reload();
            }).catch(function (error) {
                console.error('Error saving order:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditOrderModal').modal('hide');
        };
    }

})(angular.module('app.admin.order'));