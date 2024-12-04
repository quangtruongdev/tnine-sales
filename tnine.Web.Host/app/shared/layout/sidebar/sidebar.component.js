(function (app) {
    app.controller('sidebarController', sidebarController)
        .component('sidebar', {
            templateUrl: '/app/shared/layout/sidebar/sidebar.component.html',
            controller: sidebarController,
            controllerAs: 'vm',
        });

    sidebarController.$inject = ['AppMenu', 'AppMenuItem', 'serviceProxies', '$document'];

    function sidebarController(AppMenu, AppMenuItem, serviceProxies, $document) {
        var vm = this;

        vm.onClickSidebar = () => {
            const sidebar = document.querySelector(".sidebar");
            const mainContent = document.querySelector('.main-content');

            mainContent.style.marginLeft = sidebar.classList.contains('expanded') ? '5rem' : '16rem';

            sidebar.classList.toggle('expanded');
        }

        vm.loadCss = function () {
            var linkElement = $document[0].createElement('link');
            linkElement.rel = 'stylesheet';
            linkElement.href = 'app/shared/layout/sidebar/sidebar.component.css';
            $document[0].head.appendChild(linkElement);
        };

        vm.loadCss();

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
            new AppMenuItem('product', 'Products', 'product', 'fa-regular fa-shirt', [], false, 'admin', true),
            new AppMenuItem('customer', 'Customers', 'customer', 'fa-solid fa-users', [], false, 'admin', true),
            new AppMenuItem('invoice', 'Invoices', 'invoice', 'fa-light fa-file-invoice', [], false, 'admin', true),
            new AppMenuItem('order', 'Orders', 'order', 'fa-light fa-file-invoice', [], false, 'admin', true),
            new AppMenuItem('color', 'Colors', 'color', 'fa-solid fa-palette', [], false, 'admin', true),
            new AppMenuItem('size', 'Sizes', 'size', 'fa-solid fa-signal', [], false, 'admin', true),
            new AppMenuItem('shop', 'Shops', 'shop', 'fa-solid fa-shop', [], false, 'admin', true),
            new AppMenuItem('paymentMethods', 'Payment Methods', 'paymentMethods', 'fa-solid fa-users', [], false, 'admin', true),
            new AppMenuItem('paymentStatus', 'Payment Status', 'paymentStatus', 'fa-solid fa-users', [], false, 'admin', true),
            new AppMenuItem('category', 'Category', 'category', 'fa-solid fa-bars-progress', [], false, 'admin', true),

        ]);

        vm.sidebar = new AppMenu('main', 'Main Menu', menus.items);
    }

})(angular.module('app.layout'));
