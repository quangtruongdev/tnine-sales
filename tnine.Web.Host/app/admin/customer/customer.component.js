(function (app) {
    app.controller('customerController', customerController)
        .component('customer', {
            templateUrl: '/app/admin/customer/customer.component.html',
            controller: 'customerController',
            controllerAs: 'vm'
        });

    customerController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function customerController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.customers = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
            { headerName: "No", valueGetter: "node.rowIndex + 1", width: 40 },
            { headerName: "FullName", field: "FullName" },
            { headerName: "Address", field: "Address" },
            { headerName: "PhoneNumber", field: "PhoneNumber" },
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
        },
        vm.getCustomers = function () {
            serviceProxies.customerService.getAll().then(function (response) {
                vm.customers = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.customers);
                }
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