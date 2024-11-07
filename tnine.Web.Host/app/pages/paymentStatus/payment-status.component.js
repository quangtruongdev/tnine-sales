(function (app) {
    app.controller('paymentStatusController', paymentStatusController)
        .component('paymentStatus', {
            templateUrl: '/app/pages/paymentStatus/payment-status.component.html',
            controller: 'paymentStatusController',
            controllerAs: 'vm'
        });

    paymentStatusController.$inject = ['serviceProxies', '$state', '$element'];

    function paymentStatusController(serviceProxies, $state, $element) {
        var vm = this;
        vm.paymentStatus = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" }
        ];

        vm.getPaymentStatus = function () {
            serviceProxies.paymentStatusService.getAll().then(function (response) {
                vm.paymentStatus = response;
            }).catch(function (error) {
                console.error('Error fetching payment status:', error);
            });
        };

        vm.reload = function () {
            vm.getPaymentStatus();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-payment-status-modal')).controller('createOrEditPaymentStatusModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-payment-status-modal')).controller('createOrEditPaymentStatusModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.paymentStatusService.delete(vm.selectedId).then(function () {
                    vm.getPaymentStatus();
                }).catch(function (error) {
                    console.error('Error deleting payment status:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getPaymentStatus();
    }

})(angular.module('app.pages.paymentStatus'));