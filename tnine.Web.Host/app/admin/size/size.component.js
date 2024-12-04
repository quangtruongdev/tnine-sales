(function (app) {
    app.controller('sizeController', sizeController)
        .component('size', {
            templateUrl: '/app/admin/size/size.component.html',
            controller: 'sizeController',
            controllerAs: 'vm'
        });

    sizeController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function sizeController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.sizes = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Name", field: "Name", width: 120 },
                { headerName: "Description", field: "Description" }
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

        vm.getsizes = function () {
            serviceProxies.sizeService.getAll().then(function (response) {
                vm.sizes = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.sizes);
                }
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.reloads = function () {
            vm.getsizes();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-size-modal')).controller('createOrEditSizeModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-size-modal')).controller('createOrEditSizeModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.sizeService.delete(vm.selectedId).then(function () {
                    vm.getsizes();
                }).catch(function (error) {
                    console.error('Error deleting color:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getsizes();
    }

})(angular.module('app.admin.size'));