(function (app) {
    app.controller('ViewDetailModalController', ViewDetailModalController)
        .component('viewDetailModal', {
            templateUrl: '/app/admin/warehouse-receipt/view-detail-modal/view-detail-modal.component.html',
            controller: 'ViewDetailModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    ViewDetailModalController.$inject = ['serviceProxies'];

    function ViewDetailModalController(serviceProxies) {
        var vm = this;
        vm.warehouseReceipt = {};
        vm.saving = false;

        vm.columns = [
            { headerName: "ProductName", field: "ProductName" },
            { headerName: "ColorName", field: "ColorName" },
            { headerName: "SizeName", field: "SizeName" },
            { headerName: "Quantity", field: "Quantity" },
            { headerName: "Price", field: "Price" }
        ];
        vm.show = function (id) {
            if (!id) {
                vm.warehouseReceipt = {};
            } else {
                serviceProxies.warehouseReceiptService.getWarehouseReceiptForEdit(id).then(function (response) {
                    vm.warehouseReceipt = response;
                    console.log(vm.warehouseReceipt);
                }).catch(function (error) {
                    console.error('Error fetching data:', error);
                });
                serviceProxies.warehouseReceiptService.getTotal(id).then(function (response) {
                    vm.total = response;
                }).catch(function (error) {
                    console.error('Error fetching data:', error);
                });
                serviceProxies.warehouseReceiptService.getSupplier(id).then(function (response) {
                    vm.supplierName = response;
                }).catch(function (error) {
                    console.error('Error fetching data:', error);
                });
            }
            $('#viewDetailModal').modal('show');
        };

        

        vm.close = function () {
            $('#viewDetailModal').modal('hide');
        };

        
    }

})(angular.module('app.admin.warehouse-receipt'));