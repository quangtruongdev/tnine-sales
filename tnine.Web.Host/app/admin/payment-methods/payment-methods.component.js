(function (app) {
    app.controller('paymentMethodsController', paymentMethodsController)
        .component('paymentMethods', {
            templateUrl: '/app/admin/payment-methods/payment-methods.component.html',
            controller: 'paymentMethodsController',
            controllerAs: 'vm'
        });

    paymentMethodsController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function paymentMethodsController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.paymentMethods = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Name", field: "Name"}
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

        vm.getPaymentMethods = function () {
            serviceProxies.paymentMethodsService.getAll().then(function (response) {
                vm.paymentMethods = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.paymentMethods);
                }
            }).catch(function (error) {
                console.error('Error fetching payment methods:', error);
            });
        };

        vm.reloads = function () {
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