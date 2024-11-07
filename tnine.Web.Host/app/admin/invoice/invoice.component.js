(function (app) {
    app.controller('invoiceController', invoiceController)
        .component('invoice', {
            templateUrl: '/app/admin/invoice/invoice.component.html',
            controller: 'invoiceController',
            controllerAs: 'vm'
        });

    invoiceController.$inject = ['serviceProxies', '$state', '$element'];

    function invoiceController(serviceProxies, $state, $element) {
        var vm = this;
        vm.invoices = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "CreationTime", field: "CreationTime" },
            { headerName: "CustomerName", field: "CustomerName" },
            { headerName: "CustomerTelephone", field: "CustomerTelephone" },
            { headerName: "PaymentStatusName", field: "PaymentStatusName" },
            { headerName: "PaymentMethodName", field: "PaymentMethodName" },
            { headerName: "Total", field: "Total" }
        ];

        vm.getInvoices = function () {
            serviceProxies.invoiceService.getAll().then(function (response) {
                vm.invoices = response;
            }).catch(function (error) {
                console.error('Error fetching invoices:', error);
            });
        };

        vm.reload = function () {
            vm.getInvoices();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-invoice-modal')).controller('createOrEditInvoiceModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-invoice-modal')).controller('createOrEditInvoiceModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.invoiceService.delete(vm.selectedId).then(function () {
                    vm.getInvoices();
                }).catch(function (error) {
                    console.error('Error deleting invoice:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getInvoices();
    }

})(angular.module('app.admin.invoice'));