(function (app) {
    app.controller('paymentStatusController', paymentStatusController)
        .component('paymentStatus', {
            templateUrl: '/app/admin/payment-status/payment-status.component.html',
            controller: 'paymentStatusController',
            controllerAs: 'vm'
        });

    paymentStatusController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function paymentStatusController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.paymentStatus = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Name", field: "Name" }
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

        vm.getPaymentStatus = function () {
            serviceProxies.paymentStatusService.getAll().then(function (response) {
                vm.paymentStatus = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.paymentStatus);
                }
            }).catch(function (error) {
                console.error('Error fetching payment status:', error);
            });
        };

        vm.reloads = function () {
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