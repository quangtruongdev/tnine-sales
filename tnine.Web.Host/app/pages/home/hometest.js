(function (app) {
    app.component('homeTest', {
        templateUrl: '/app/pages/home/hometest.html',
        controller: homeTestController,
        controllerAs: 'vm',
    });

    homeTestController.$inject = [];

    function homeTestController() {
        var vm = this;

        vm.message = "Hello World!";
    }

})(angular.module('tnine.home'));