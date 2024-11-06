(function (app) {
    app.controller('topbarController', topbarController)
        .component('topbar', {
        templateUrl: '/app/shared/layout/topbar/topbar.component.html',
        controller: topbarController,
        controllerAs: 'vm'
        });

    topbarController.$inject = [];

    function topbarController() {
        var vm = this;
    }

})(angular.module('app.layout'));