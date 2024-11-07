(function (app) {
    app.controller('userController', userController)
        .component('user', {
            templateUrl: '/app/admin/authorization/user/user.component.html',
            controller: 'userController',
            controllerAs: 'vm'
        });

    userController.$inject = ['serviceProxies'];

    function userController(serviceProxies) {
        var vm = this;
        vm.users = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "UserId", field: "Id" },
            { headerName: "Username", field: "UserName" },
            { headerName: "Location", field: "Location" },
            { headerName: "Email", field: "Email" },
            { headerName: "Phone Number", field: "PhoneNumber" },
            { headerName: "Join Date", field: "JoinDate" },
            { headerName: "Status", field: "Status" },
        ];

        vm.getUsers = function () {
            serviceProxies.userService.getAll().then(function (response) {
                vm.users = response;
            }).catch(function (error) {
                console.error('Error fetching users:', error);
            });
        };

        vm.reload = function () {
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
    }


})(angular.module('app.admin.authorization.user'));