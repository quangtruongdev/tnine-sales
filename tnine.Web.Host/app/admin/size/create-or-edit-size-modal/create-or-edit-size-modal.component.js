(function (app) {
    app.controller('CreateOrEditSizeModalController', CreateOrEditSizeModalController)
        .component('createOrEditSizeModal', {
            templateUrl: '/app/admin/size/create-or-edit-size-modal/create-or-edit-size-modal.component.html',
            controller: 'CreateOrEditSizeModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditSizeModalController.$inject = ['serviceProxies'];

    function CreateOrEditSizeModalController(serviceProxies) {
        var vm = this;
        vm.size = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.size = {};
            } else {
                serviceProxies.sizeService.getById(id).then(function (response) {
                    vm.size = response;
                }).catch(function (error) {
                    console.error('Error fetching color:', error);
                });
            }
            $('#createOrEditSizeModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.sizeService.createOrEdit(vm.size).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving color:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditSizeModal').modal('hide');
        };
    }

})(angular.module('app.admin.size'));