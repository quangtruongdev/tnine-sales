(function (app) {
    app.controller('loginController', loginController)
        .component('login', {
            templateUrl: '/app/pages/login/login.component.html',
            controller: loginController,
            controllerAs: 'vm'
        });

    loginController.$inject = ['$scope', 'serviceProxies', '$state', '$element'];

})(angular.module('tnine.auth'));