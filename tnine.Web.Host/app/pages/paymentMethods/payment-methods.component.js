(function (app) {
    app.controller('paymentMethodsController', paymentMethodsController)
        .component('paymentMethods', {
            templateUrl: '/app/pages/paymentMethods/payment-methods.component.html',
            controller: 'paymentMethodsController',
            controllerAs: 'vm'
        });

    paymentMethodsController.$inject = ['serviceProxies', '$state', '$element'];

    function paymentMethodsController(serviceProxies, $state, $element) {
        var vm = this;
        vm.paymentMethods = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" }
        ];

        vm.getPaymentMethods = function () {
            serviceProxies.paymentMethodsService.getAll().then(function (response) {
                vm.paymentMethods = response;
            }).catch(function (error) {
                console.error('Error fetching payment methods:', error);
            });
        };

        vm.reload = function () {
            vm.getPaymentMethods();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-payment-methods-modal')).controller('createOrEditPaymentMethodsModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-payment-methods-modal')).controller('createOrEditPaymentMethodsModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.paymentMethodsService.delete(vm.selectedId).then(function () {
                    vm.getPaymentMethods();
                }).catch(function (error) {
                    console.error('Error deleting payment methods:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getPaymentMethods();
    }

})(angular.module('app.pages.paymentMethods'));