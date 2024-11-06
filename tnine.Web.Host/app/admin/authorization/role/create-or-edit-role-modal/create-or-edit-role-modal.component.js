(function (app) {
    app.controller('CreateOrEditRoleModalController', CreateOrEditRoleModalController)
        .component('createOrEditRoleModal', {
            templateUrl: '/app/admin/authorization/role/create-or-edit-role-modal/create-or-edit-role-modal.component.html',
            controller: 'CreateOrEditRoleModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditRoleModalController.$inject = ['serviceProxies'];

    function CreateOrEditRoleModalController(serviceProxies) {
        var vm = this;
        vm.role = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.role = {};
            } else {
                serviceProxies.roleService.getById(id).then(function (response) {
                    vm.role = response.Role;
                }).catch(function (error) {
                    console.error('Error fetching role:', error);
                });
            }
            $('#createOrEditRoleModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.roleService.createOrEdit(vm.role).then(function () {
                vm.close();
            }).catch(function (error) {
                console.error('Error saving role:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditRoleModal').modal('hide');
        };
    }

})(angular.module('app.admin.authorization.role'));