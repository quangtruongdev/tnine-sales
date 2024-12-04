(function (app) {
    app.controller('colorController', colorController)
        .component('color', {
            templateUrl: '/app/admin/color/color.component.html',
            controller: 'colorController',
            controllerAs: 'vm'
        });

    colorController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];;

    function colorController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.colors = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Code", field: "Code", width: 120 },
                {
                    headerName: "HexCode",
                    field: 'HexCode',
                    cellRenderer: function (params) {
                        const color = params.value || '#FFFFFF'; // Giá trị mặc định
                        return `<div style="background-color: ${color}; width: 45px; height: 45px;"></div>`;
                    }
                },
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

        vm.getcolors = function () {
            serviceProxies.colorService.getAll().then(function (response) {
                vm.colors = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.colors);
                }
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.reloads = function () {
            vm.getcolors();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-color-modal')).controller('createOrEditColorModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-color-modal')).controller('createOrEditColorModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.colorService.delete(vm.selectedId).then(function () {
                    vm.getcolors();
                }).catch(function (error) {
                    console.error('Error deleting color:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getcolors();
    }

})(angular.module('app.admin.color'));