(function (app) {
    app.controller('CreateOrEditWarehouseReceiptModalController', CreateOrEditWarehouseReceiptModalController)
        .component('createOrEditWarehouseReceiptModal', {
            templateUrl: '/app/admin/warehouse-receipt/create-or-edit-warehouse-receipt-modal/create-or-edit-warehouse-receipt-modal.component.html',
            controller: 'CreateOrEditWarehouseReceiptModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditWarehouseReceiptModalController.$inject = ['serviceProxies'];

    function CreateOrEditWarehouseReceiptModalController(serviceProxies) {
        var vm = this;
        vm.warehouseReceipt = {};
        vm.saving = false;
        vm.ProductInWarehouseReceipts = [];

        vm.show = function () {
            vm.SupplierId = null;
            vm.ProductInWarehouseReceipts = [];
            vm.getListSuppliers();
            vm.getListProducts();
            vm.getListColors();
            vm.getListSizes();
            $('#createOrEditWarehouseReceiptModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            vm.warehouseReceipt = {
                SupplierId: vm.SupplierId,
                ProductInWarehouseReceipts: vm.ProductInWarehouseReceipts,
            }
            serviceProxies.warehouseReceiptService.createOrEdit(vm.warehouseReceipt).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving data:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditWarehouseReceiptModal').modal('hide');
        };

        vm.isSaveDisabled = function () {
            // Kiểm tra các trường chính
            //if (!vm.color.HexCode || !vm.color.Code) {
            //    return true;
            //}

            //return false;
        };
        vm.addProduct = function () {
            if (!Array.isArray(vm.ProductInWarehouseReceipts)) {
                vm.ProductInWarehouseReceipts = [];
            }

            vm.ProductInWarehouseReceipts.push({
                ProductId: null,
                Price: null
            });
            vm.ProductInWarehouseReceipts.VariationProducts = [{
                ColorId: null,
                SizeId: null,
                Quantity: null
            }];
        };
        vm.getListProducts = function () {
            serviceProxies.warehouseReceiptService.getListProducts().then(function (response) {
                vm.listProducts = response.Results;
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.addProductVariation = function (product) {
            if (!Array.isArray(product.VariationProducts)) {
                product.VariationProducts = [];
            }

            product.VariationProducts.push({
                ColorId: null,
                SizeId: null,
                Quantity: null
            });
        };

        vm.getListColors = function () {
            serviceProxies.warehouseReceiptService.getListColors().then(function (response) {
                vm.listColors = response;
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.getListSizes = function () {
            serviceProxies.warehouseReceiptService.getListSizes().then(function (response) {
                vm.listSizes = response;
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.removeProduct = function (index) {
            if (vm.ProductInWarehouseReceipts && vm.ProductInWarehouseReceipts.length > index) {
                vm.ProductInWarehouseReceipts.splice(index, 1);
            }
        };

        vm.getListSuppliers = function () {
            serviceProxies.warehouseReceiptService.getListSuppliers().then(function (response) {
                vm.listSuppliers = response;
                console.log(vm.listSuppliers);
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };
        

    }

})(angular.module('app.admin.warehouse-receipt'));