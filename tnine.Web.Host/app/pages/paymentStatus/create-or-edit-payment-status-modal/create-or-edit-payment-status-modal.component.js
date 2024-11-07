(function (app) {
    app.controller('CreateOrEditPaymentStatusModalController', CreateOrEditPaymentStatusModalController)
        .component('createOrEditPaymentStatusModal', {
            templateUrl: '/app/pages/paymentStatus/create-or-edit-payment-status-modal/create-or-edit-payment-status-modal.component.html',
            controller: 'CreateOrEditPaymentStatusModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditPaymentStatusModalController.$inject = ['serviceProxies'];

    function CreateOrEditPaymentStatusModalController(serviceProxies, $element) {
        var vm = this;
        vm.paymentStatus = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.paymentStatus = {};
            } else {
                serviceProxies.paymentStatusService.getById(id).then(function (response) {
                    vm.paymentStatus = response.PaymentStatus;
                }).catch(function (error) {
                    console.error('Error fetching payment status:', error);
                });
            }
            $('#createOrEditPaymentStatusModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.paymentStatusService.createOrEdit(vm.paymentStatus).then(function () {
                vm.close();
            }).catch(function (error) {
                console.error('Error saving payment status:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditPaymentStatusModal').modal('hide');
        };
    }

})(angular.module('app.pages.paymentStatus'));