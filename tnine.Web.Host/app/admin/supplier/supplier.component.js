(function (app) {
    app.controller('supplierController', supplierController)
        .component('supplier', {
            templateUrl: '/app/admin/supplier/supplier.component.html',
            controller: 'supplierController',
            controllerAs: 'vm'
        });

    supplierController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];;

    function supplierController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.suppliers = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Name", field: "Name" },
                { headerName: "Address", field: "Address" },
                { headerName: "PhoneNumber", field: "PhoneNumber" },
            ],
            rowData: [],
            pagination: true,
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

        vm.getsuppliers = function () {
            serviceProxies.supplierService.getAll().then(function (response) {
                vm.suppliers = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.suppliers);
                }
            }).catch(function (error) {
                console.error('Error fetching suppliers:', error);
            });
        };

        vm.reloads = function () {
            vm.getsuppliers();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-supplier-modal')).controller('createOrEditSupplierModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-supplier-modal')).controller('createOrEditSupplierModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.supplierService.delete(vm.selectedId).then(function () {
                    vm.getsuppliers();
                }).catch(function (error) {
                    console.error('Error deleting supplier:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getsuppliers();
    }

})(angular.module('app.admin.supplier'));