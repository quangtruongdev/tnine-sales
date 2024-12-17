(function (app) {
    app.controller('CreateOrEditCustomerModalController', CreateOrEditCustomerModalController)
        .component('createOrEditCustomerModal', {
            templateUrl: '/app/admin/customer/create-or-edit-customer-modal/create-or-edit-customer-modal.component.html',
            controller: 'CreateOrEditCustomerModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditCustomerModalController.$inject = ['serviceProxies'];

    function CreateOrEditCustomerModalController(serviceProxies) {
        var vm = this;
        vm.customer = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.customer = {};
            } else {
                serviceProxies.customerService.getCustomerForEdit(id).then(function (response) {
                    vm.customer = response.Customer;
                }).catch(function (error) {
                    console.error('Error fetching customer:', error);
                });
            }
            $('#createOrEditCustomerModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.customerService.createOrEdit(vm.customer).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving customer:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditCustomerModal').modal('hide');
        };

        vm.isSaveDisabled = function () {
            if (!vm.customer.FullName || !vm.customer.Address || !vm.customer.PhoneNumber) {
                return true;
            }

            return false;
        };
    }

})(angular.module('app.admin.customer'));