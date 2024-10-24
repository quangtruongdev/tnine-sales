(function () {
    'use strict';

    angular
        .module('tnine.layout')
        .component('sidebar', {
            templateUrl: '/app/shared/layout/sidebar/sidebar.component.html',
            controller: SidebarController,
            controllerAs: 'vm',
            bindings: {
                user: '<'
            }
        });


    
})();