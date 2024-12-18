(function (app) {
    app.controller('userController', userController)
        .component('user', {
            templateUrl: '/app/admin/authorization/user/user.component.html',
            controller: 'userController',
            controllerAs: 'vm'
        });

    userController.$inject = ['serviceProxies', '$timeout'];

    function userController(serviceProxies, $timeout) {
        var vm = this;
        vm.users = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1" },
                { headerName: "Username", field: "Username" },
                { headerName: "Email", field: "Email" },
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

        vm.getUsers = function () {
            serviceProxies.userService.getAll().then(function (response) {
                vm.users = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.users);
                }
            }).catch(function (error) {
                console.error('Error fetching users:', error);
            });
        };

        vm.reloads = function () {
            vm.getUsers();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-user-modal')).controller('createOrEditUserModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-user-modal')).controller('createOrEditUserModal').show(vm.selectedId);
        };

        vm.delete = function (id) {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.userService.delete(id).then(function () {
                    vm.getUsers();
                }).catch(function (error) {
                    console.error('Error deleting user:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getUsers();
    }


})(angular.module('app.admin.authorization.user'));