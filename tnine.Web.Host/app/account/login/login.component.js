(function (app) {
    app.controller('loginController', loginController)
        .component('login', {
            templateUrl: '/app/account/login/login.component.html',
            controller: loginController,
            controllerAs: 'vm',
        });

    loginController.$inject = ['serviceProxies', '$state'];

    function loginController(serviceProxies, $state) {
        var vm = this;

        vm.loginData = {
            email: '',
            password: ''
        };

        vm.login = function () {
            var res = serviceProxies.accountService.login(vm.loginData);
            res.then(function (result) {
                if (result.success) {
                    $state.go('home');
                }
            });
        }
    }

})(angular.module('app.account'));