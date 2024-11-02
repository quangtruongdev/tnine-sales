(function (app) {
    app.controller('createOrEditAccountModalController', createOrEditAccountModalController)
        .component('createOrEditAccountModal', {
            templateUrl: '/app/admin/authorization/account/create-or-edit-account-modal/create-or-edit-account-modal.html',
            controller: 'createOrEditAccountModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    createOrEditAccountModalController.$inject = ['serviceProxies', '$element'];

    function createOrEditAccountModalController(serviceProxies, $element) {
        var vm = this;
        vm.account = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.account = {};
            } else {
                serviceProxies.accountService.getById(id).then(function (response) {
                    vm.account = response.Account;
                }).catch(function (error) {
                    console.error('Error fetching account:', error);
                });
            }
            $('#createOrEditAccountModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.accountService.createOrUpdate(vm.account).then(function () {
                vm.close();
            }).catch(function (error) {
                console.error('Error saving account:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditAccountModal').modal('hide');
        };
    }

})(angular.module('app.admin.authorization.account'));