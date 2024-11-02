(function (app) {
    app.controller('createOrEditPermissionModalController', createOrEditPermissionModalController)
        .component('createOrEditPermissionModal', {
            templateUrl: '/app/admin/authorization/permission/create-or-edit-permission-modal/create-or-edit-permission-modal.component.html',
            controller: createOrEditPermissionModalController,
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            }
        });

    createOrEditPermissionModalController.$inject = ['serviceProxies'];

    function createOrEditPermissionModalController(serviceProxies, $element) {
        var vm = this;
        vm.permission = {};
        vm.permissions = [];
        vm.saving = false;

        vm.show = function (id) {
            serviceProxies.permissionService.getAll().then(function (response) {
                vm.permissions = response;
            }).catch(function (error) {
                console.error('Error fetching permissions:', error);
            });

            if (!id) {
                vm.permission = {};
            } else {
                serviceProxies.permissionService.getById(id).then(function (response) {
                    vm.permission = response.Permission;
                }).catch(function (error) {
                    console.error('Error fetching permission:', error);
                });
            }

            $('#createOrEditPermissionModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.permissionService.createOrUpdate(vm.permission).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving permission:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditPermissionModal').modal('hide');
        }
    }

})(angular.module('app.admin.authorization.permission'));