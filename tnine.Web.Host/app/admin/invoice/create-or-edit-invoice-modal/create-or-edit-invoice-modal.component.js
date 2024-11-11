
(function (app) {
    app.controller('CreateOrEditInvoiceModalController', CreateOrEditInvoiceModalController)
        .component('createOrEditInvoiceModal', {
            templateUrl: '/app/admin/invoice/create-or-edit-invoice-modal/create-or-edit-invoice-modal.component.html',
            controller: 'CreateOrEditInvoiceModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditInvoiceModalController.$inject = ['serviceProxies'];

    function CreateOrEditInvoiceModalController(serviceProxies, $element) {
        var vm = this;
        vm.invoice = {};
        vm.saving = false;
        vm.listPaymentStatus;
        vm.listPaymentMethods;

        vm.show = function (id) {
            if (!id) {
                vm.invoice = {};
            } else {
                serviceProxies.invoiceService.getInvoiceForEdit(id).then(function (response) {
                    vm.invoice = response.Invoice;
                }).catch(function (error) {
                    console.error('Error fetching invoice:', error);
                });
            }

            serviceProxies.paymentStatusService.getAll().then(function (response) {
                vm.listPaymentStatus = response;
            }).catch(function (error) {
                console.error('Error fetching payment status:', error);
            });

            serviceProxies.paymentMethodsService.getAll().then(function (response) {
                vm.listPaymentMethods = response;
            }).catch(function (error) {
                console.error('Error fetching payment status:', error);
            });

            $('#createOrEditInvoiceModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.invoiceService.createOrEdit(vm.invoice).then(function () {
                vm.close();
                location.reload();
            }).catch(function (error) {
                console.error('Error saving invoice:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditInvoiceModal').modal('hide');
        };
    }

})(angular.module('app.admin.invoice'));