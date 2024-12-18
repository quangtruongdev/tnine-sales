(function (app) {
    app.controller('CreateOrEditSupplierModalController', CreateOrEditSupplierModalController)
        .component('createOrEditSupplierModal', {
            templateUrl: '/app/admin/supplier/create-or-edit-supplier-modal/create-or-edit-supplier-modal.component.html',
            controller: 'CreateOrEditSupplierModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditSupplierModalController.$inject = ['serviceProxies'];

    function CreateOrEditSupplierModalController(serviceProxies) {
        var vm = this;
        vm.color = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.supplier = {};
            } else {
                serviceProxies.supplierService.getById(id).then(function (response) {
                    vm.supplier = response;
                }).catch(function (error) {
                    console.error('Error fetching supplier:', error);
                });
            }
            $('#createOrEditSupplierModal').modal('show');
        };

        vm.isFormValid = function () {
            return vm.supplier && vm.supplier.Name && vm.supplier.Address && vm.supplier.PhoneNumber;
        };
        
        vm.save = function () {
            vm.saving = true;
            serviceProxies.supplierService.createOrEdit(vm.supplier).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving supplier:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditSupplierModal').modal('hide');
        };
        vm.isSaveDisabled = function () {
           
            if (!vm.supplier.Name || !vm.supplier.Address || !vm.supplier.PhoneNumber) {
                return true;
            }

            return false;
        };
    }

})(angular.module('app.admin.supplier'));