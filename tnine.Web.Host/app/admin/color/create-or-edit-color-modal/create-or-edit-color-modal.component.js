(function (app) {
    app.controller('CreateOrEditColorModalController', CreateOrEditColorModalController)
        .component('createOrEditColorModal', {
            templateUrl: '/app/admin/color/create-or-edit-color-modal/create-or-edit-color-modal.component.html',
            controller: 'CreateOrEditColorModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditColorModalController.$inject = ['serviceProxies'];

    function CreateOrEditColorModalController(serviceProxies) {
        var vm = this;
        vm.color = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.color = {};
            } else {
                serviceProxies.colorService.getById(id).then(function (response) {
                    vm.color = response;
                }).catch(function (error) {
                    console.error('Error fetching color:', error);
                });
            }
            $('#createOrEditColorModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.colorService.createOrEdit(vm.color).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving color:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditColorModal').modal('hide');
        };

        vm.isSaveDisabled = function () {
            if (!vm.color.HexCode || !vm.color.Code) {
                return true;
            }

            return false;
        };
    }

})(angular.module('app.admin.color'));