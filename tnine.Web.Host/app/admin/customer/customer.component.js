(function (app) {
    app.controller('customerController', customerController)
        .component('customer', {
            templateUrl: '/app/admin/customer/customer.component.html',
            controller: 'customerController',
            controllerAs: 'vm'
        });

    customerController.$inject = ['serviceProxies'];

    function customerController(serviceProxies) {
        var vm = this;
        vm.customers = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Username", field: "Username" },
            { headerName: "FullName", field: "FullName" },
            { headerName: "Address", field: "Address" },
            { headerName: "PhoneNumber", field: "PhoneNumber" }
        ];

        vm.getCustomers = function () {
            serviceProxies.customerService.getAll().then(function (response) {
                vm.customers = response;
            }).catch(function (error) {
                console.error('Error fetching customers:', error);
            });
        };

        vm.reloads = function () {
            vm.getCustomers();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-customer-modal')).controller('createOrEditCustomerModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-customer-modal')).controller('createOrEditCustomerModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.customerService.delete(vm.selectedId).then(function () {
                    vm.getCustomers();
                }).catch(function (error) {
                    console.error('Error deleting customer:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getCustomers();
    }

})(angular.module('app.admin.customer'));