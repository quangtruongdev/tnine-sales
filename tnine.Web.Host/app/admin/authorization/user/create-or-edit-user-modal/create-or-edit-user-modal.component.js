(function (app) {
    app.controller('createOrEditUserModalController', createOrEditUserModalController)
    .component('createOrEditUserModal', {
            templateUrl: '/app/admin/authorization/user/create-or-edit-user-modal/create-or-edit-user-modal.component.html',
            controller: 'createOrEditUserModalController',
            controllerAs: 'vm',
            bindings: {
               onSaved: '&'
            }
    });

    createOrEditUserModalController.$inject = ['serviceProxies'];

    function createOrEditUserModalController(serviceProxies) {
        var vm = this;
        vm.user = {
            RoleId: null
        };
        vm.saving = false;
        vm.roles = [];

        vm.columns = [
            { headerName: "UserName", field: "UserName" },
            { headerName: "Email", field: "Email" },
        ];

        vm.show = function (id) {
            serviceProxies.roleService.getAll().then(function (response) {
                vm.roles = response;
            }).catch(function (error) {
                console.error('Error fetching roles:', error);
            });

            if (!id) {
                vm.user = {};
            } else {
                serviceProxies.userService.get(id).then(function (response) {
                    vm.user = response;
                }).catch(function (error) {
                    console.error('Error fetching user:', error);
                });
            }
            $('#createOrEditUserModal').modal('show');
        };

        vm.save = function () {
            console.log(vm.user);
        };

        vm.close = function () {
            $('#createOrEditUserModal').modal('hide');
        }
    }

})(angular.module('app.admin.authorization.user'))