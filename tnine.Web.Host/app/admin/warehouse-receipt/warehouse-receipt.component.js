(function (app) {
    app.controller('warehouseReceiptController', warehouseReceiptController)
        .component('warehouseReceipt', {
            templateUrl: '/app/admin/warehouse-receipt/warehouse-receipt.component.html',
            controller: 'warehouseReceiptController',
            controllerAs: 'vm'
        });

    warehouseReceiptController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function warehouseReceiptController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.warehouseReceipts = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1" },
                { headerName: "Supplier", field: "SupplierName" },
                { headerName: "CreationTime", field: "CreationTime" },
                { headerName: "Total", field: "Total" },
            ],
            rowData: [],
            pagination: true,
            paginationPageSize: 10,
            defaultColDef: {
                sortable: true,
                filter: true,
                resizable: true,
                floatingFilter: true,
                flex: 1,
                cellStyle: {
                    display: 'flex',
                    alignItems: 'center',
                }
            },
            rowSelection: 'single',
            enableColResize: true,
            rowHeight: 35,
            frameworkComponents: {
            },
            onGridReady: function (params) {
                vm.gridApi = params.api
                window.addEventListener('resize', () => params.api.sizeColumnsToFit())
            },
            onFirstDataRendered: function (params) {
                params.api.sizeColumnsToFit();
            },
            onRowClicked: function (event) {
                $timeout(() => vm.selectedId = event.data.Id)
            }
        };
        vm.getWarehouseReceipts = function () {
            serviceProxies.warehouseReceiptService.getAll()
                .then(function (response) {
                    vm.warehouseReceipts = response.Results;
                    if (vm.gridApi) {
                        vm.gridApi.setRowData(vm.warehouseReceipts);
                    }
                }).catch(function (erorr) {
                    console.error('Error fetching data:', erorr);
                });
        };
        vm.reloads = function () {
            vm.getWarehouseReceipts();
        };
        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-warehouse-receipt-modal')).controller('createOrEditWarehouseReceiptModal').show();
        }
        vm.viewDetail = function () {
            angular.element(document.querySelector('view-detail-modal')).controller('viewDetailModal').show(vm.selectedId);
        }
        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.warehouseReceiptService.delete(vm.selectedId)
                    .then(function () {
                        vm.reloads();
                    }).catch(function (error) {
                        console.error('Error deleting item:', error);
                    });
            }
        }
        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getWarehouseReceipts();
    }
})(angular.module('app.admin.warehouse-receipt'));