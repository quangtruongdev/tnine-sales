(function (app) {
    app.controller('orderController', orderController)
        .component('order', {
            templateUrl: '/app/admin/order/order.component.html',
            controller: 'orderController',
            controllerAs: 'vm'
        });

    orderController.$inject = ['serviceProxies', '$state', '$element'];

    function orderController(serviceProxies, $state, $element) {
        var vm = this;
        vm.orders = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "CreationTime", field: "CreationTime" },
            { headerName: "CustomerName", field: "CustomerName" },
            { headerName: "CustomerTelephone", field: "CustomerTelephone" },
            { headerName: "Total", field: "Total" }
        ];

        vm.getOrders = function () {
            serviceProxies.orderService.getAll().then(function (response) {
                vm.orders = response;
            }).catch(function (error) {
                console.error('Error fetching orders:', error);
            });
        };

        vm.reload = function () {
            vm.getOrders();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-order-modal')).controller('createOrEditOrderModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-order-modal')).controller('createOrEditOrderModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.orderService.delete(vm.selectedId).then(function () {
                    vm.getOrders();
                }).catch(function (error) {
                    console.error('Error deleting order:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getOrders();
    }

})(angular.module('app.admin.order'));