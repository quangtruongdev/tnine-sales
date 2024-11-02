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
            username: '',
            password: ''
        };

        vm.login = function () {
            serviceProxies.accountService.login(vm.loginData).then(function (response) {
                $state.go('home');
            }).catch(function (error) {
                console.error('Error logging in:', error);
            });
        }
    }

})(angular.module('app.account'));