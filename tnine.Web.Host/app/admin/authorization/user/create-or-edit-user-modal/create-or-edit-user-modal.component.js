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
        vm.user = {};
        vm.saving = false;
        vm.roles = [];

        vm.show = function (id) {
            serviceProxies.roleService.getAll().then(function (response) {
                vm.roles = response;
            }).catch(function (error) {
                console.error('Error fetching roles:', error);
            });

            if (!id) {
                vm.user = {};
            } else {
                serviceProxies.userService.getById(id).then(function (response) {
                    vm.user = response;
                }).catch(function (error) {
                    console.error('Error fetching user:', error);
                });
            }
            $('#createOrEditUserModal').modal('show');
        };

        vm.isValid = function () {
            if (!vm.user.Id) {
                return vm.user.Email && vm.user.Password && vm.user.Password.length >= 6;
            }
        };

        vm.save = function () {
            vm.saving = true;

            vm.user.Roles = vm.roles.filter(function (role) {
                return role.selected
            }).map(function (role) {
                return role.Name;
            });

            if (!vm.user.Id) {
                serviceProxies.accountService.register(vm.user).then(function () {
                    vm.onSaved();
                    vm.close();
                }).catch(function (error) {
                    console.error('Error saving user:', error);
                }).finally(function () {
                    vm.saving = false;
                });
            } else {
                serviceProxies.userService.createOrEdit(vm.user).then(function () {
                    vm.onSaved();
                    vm.close();
                }).catch(function (error) {
                    console.error('Error saving user:', error);
                }).finally(function () {
                    vm.saving = false;
                });
            }
        };


        vm.close = function () {
            $('#createOrEditUserModal').modal('hide');
        }
    }

})(angular.module('app.admin.authorization.user'))