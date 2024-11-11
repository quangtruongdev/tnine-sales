(function (app) {
    app.controller('CreateOrEditPaymentMethodsModalController', CreateOrEditPaymentMethodsModalController)
        .component('createOrEditPaymentMethodsModal', {
            templateUrl: '/app/admin/payment-methods/create-or-edit-payment-methods-modal/create-or-edit-payment-methods-modal.component.html',
            controller: 'CreateOrEditPaymentMethodsModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditPaymentMethodsModalController.$inject = ['serviceProxies'];

    function CreateOrEditPaymentMethodsModalController(serviceProxies, $element) {
        var vm = this;
        vm.paymentMethods = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.paymentMethods = {};
            } else {
                serviceProxies.paymentMethodsService.getById(id).then(function (response) {
                    vm.paymentMethods = response.PaymentMethods;
                }).catch(function (error) {
                    console.error('Error fetching payment methods:', error);
                });
            }
            $('#createOrEditPaymentMethodsModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.paymentMethodsService.createOrEdit(vm.paymentMethods).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving payment methods:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditPaymentMethodsModal').modal('hide');
        };
    }

})(angular.module('app.pages.paymentMethods'));