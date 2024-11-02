(function (app) {
    app.controller('permissionController', permissionController)
        .component('permission', {
            templateUrl: '/app/admin/authorization/permission/permission.component.html',
            controller: 'permissionController',
            controllerAs: 'vm'
        });

    permissionController.$inject = ['serviceProxies', '$state', '$element'];

    function permissionController(serviceProxies, $state, $element) {
        var vm = this;
        vm.permissions = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" },
        ];

        vm.getPermissions = function () {
            serviceProxies.permissionService.getAll().then(function (response) {
                vm.permissions = response;
            }).catch(function (error) {
                console.error('Error fetching permissions:', error);
            });
        };

        vm.reload = function () {
            vm.getPermissions();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-permission-modal')).controller('createOrEditPermissionModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-permission-modal')).controller('createOrEditPermissionModal').show(vm.selectedId);
        };

        vm.delete = function (id) {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.permissionService.delete(id).then(function () {
                    vm.getPermissions();
                }).catch(function (error) {
                    console.error('Error deleting permission:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getPermissions();
    }

})(angular.module('app.admin.authorization.permission'));