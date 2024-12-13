(function (app) {
    app.controller('topbarController', topbarController)
        .component('topbar', {
            templateUrl: '/app/shared/layout/topbar/topbar.component.html',
            controller: topbarController,
            controllerAs: 'vm'
        });

    topbarController.$inject = ['$document', 'serviceProxies', '$state'];

    function topbarController($document, serviceProxies, $state) {
        var vm = this;

        vm.loadCss = function () {
            var linkElement = $document[0].createElement('link');
            linkElement.rel = 'stylesheet';
            linkElement.href = 'app/shared/layout/topbar/topbar.component.css';
            $document[0].head.appendChild(linkElement);
        };

        vm.loadCss();

        vm.logout = function () {
            serviceProxies.accountService.logout().then(function () {
                var accountInfo = sessionStorage.getItem('accountInfo');
                if (accountInfo) {
                    sessionStorage.removeItem('accountInfo');
                }
                $state.go('login');
            });
        };
    }

})(angular.module('app.layout'));