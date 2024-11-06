(function (app) {
    app.controller('sidebarController', sidebarController)
        .component('sidebar', {
            templateUrl: '/app/shared/layout/sidebar/sidebar.component.html',
            controller: sidebarController,
            controllerAs: 'vm',
        });

    sidebarController.$inject = ['AppMenu', 'AppMenuItem', 'serviceProxies'];

    function sidebarController(AppMenu, AppMenuItem, serviceProxies) {
        var vm = this;

        //const menus = new AppMenu('main', 'Main Menu', [
        //    new AppMenuItem('home', 'Home', 'home', 'fa-solid fa-house', [], false, '', false),
        //    new AppMenuItem('authorization', 'Authorization', '', 'fa-solid fa-user-lock', [
        //        new AppMenuItem('roles', 'Roles', 'role', 'fa-solid fa-users', [], false, 'Pages.Roles', true),
        //        new AppMenuItem('users', 'Users', 'user', 'fa-solid fa-user', [], false, 'Pages.Users', true),
        //    ], true, 'Pages.Authorization', true),
        //    new AppMenuItem('todo', 'Todos', 'todo', 'fa-solid fa-pen', [], false, 'Pages.Todos', true),
        //    new AppMenuItem('customer', 'Customers', 'customer', 'fa-solid fa-users', [], false, 'Pages.Customers', true)
        //]);

        const menus = new AppMenu('main', 'Main Menu', [
            new AppMenuItem('home', 'Home', 'home', 'fa-solid fa-house', [], false, '', false),
            new AppMenuItem('authorization', 'Authorization', '', 'fa-solid fa-user-lock', [
                new AppMenuItem('roles', 'Roles', 'role', 'fa-solid fa-users', [], false, 'admin', true),
                new AppMenuItem('users', 'Users', 'user', 'fa-solid fa-user', [], false, 'admin', true),
                new AppMenuItem('permissions', 'Permissions', 'permission', 'fa-solid fa-user-lock', [], false, 'admin', true),
            ], true, 'admin', true),
            new AppMenuItem('todo', 'Todos', 'todo', 'fa-solid fa-pen', [], false, 'admin', true),
            new AppMenuItem('customer', 'Customers', 'customer', 'fa-solid fa-users', [], false, 'admin', true)
        ]);

        vm.sidebar = new AppMenu('main', 'Main Menu', menus.items);
    }

})(angular.module('app.layout'));
