(function (app) {
    app.controller('loginController', loginController)
        .component('login', {
            templateUrl: '/app/account/login/login.component.html',
            controller: loginController,
            controllerAs: 'vm',
            bindToController: true
        });

    loginController.$inject = ['serviceProxies', '$state'];

    function loginController(serviceProxies, $state) {
        var vm = this;

        vm.loginData = {
            email: '',
            password: '',
            remmeberMe: false
        };

        vm.$onInit = function () {
            vm.getAccountInfo();
        };

        vm.getAccountInfo = function () {
            serviceProxies.accountService.getAccountInfo()
                .then(function (result) {
                    sessionStorage.setItem('accountInfo', JSON.stringify(result));
                    if (result.IsAuthenticated) {
                        $state.go('home');
                    }
                });
        }

        vm.login = function () {
            serviceProxies.accountService.login(vm.loginData)
            .then(function (result) {
                if (result.Success = true) {
                    vm.getAccountInfo();
                }
            });
        }
    }

})(angular.module('app.account'));