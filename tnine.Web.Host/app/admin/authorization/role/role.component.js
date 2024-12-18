(function (app) {
    app.controller('roleController', roleController)
        .component('role', {
            templateUrl: '/app/admin/authorization/role/role.component.html',
            controller: 'roleController',
            controllerAs: 'vm'
        });

    roleController.$inject = ['serviceProxies', '$timeout'];

    function roleController(serviceProxies, $timeout) {
        var vm = this;
        vm.roles = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Name", field: "Name", width: 100 },
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

        vm.getRoles = function () {
            serviceProxies.roleService.getAll().then(function (response) {
                vm.roles = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.roles);
                }
            }).catch(function (error) {
                console.error('Error fetching roles:', error);
            });
        };

        vm.reloads = function () {
            vm.getRoles();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-role-modal')).controller('createOrEditRoleModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-role-modal')).controller('createOrEditRoleModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.roleService.delete(vm.selectedId).then(function () {
                    vm.getRoles();
                }).catch(function (error) {
                    console.error('Error deleting role:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getRoles();
    }

})(angular.module('app.admin.authorization.role'));