(function (app) {
    app.controller('accountController', accountController)
        .component('account', {
            templateUrl: 'app/admin/authorization/account/account.template.html',
            controller: 'accountController',
            controllerAs: 'vm'
        });

    accountController.$inject = ['serviceProxies', '$state', '$element'];

    function accountController(serviceProxies, $state, $element) {
        var vm = this;
        vm.accounts = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Username", field: "Username" },
            { headerName: "Email", field: "Email" },
        ];

        vm.$onInit = function () {
            vm.loads();
        };

        vm.loads = function () {
            serviceProxies.account.getAll().then(function (response) {
                vm.accounts = response;
            });
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-account-modal')).controller('createOrEditAccountModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-account-modal')).controller('createOrEditAccountModal').show(vm.selectedId);
        };

        vm.delete = function (id) {
            serviceProxies.account.delete(id).then(function () {
                vm.accounts = vm.accounts.filter(function (account) {
                    return account.id !== id;
                });
            });
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.id;
        };
    }

})(angular.module('app.admin.authorization.account'));