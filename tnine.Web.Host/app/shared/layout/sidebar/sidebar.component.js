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

        vm.loadCss = function () {
            var linkElement = $document[0].createElement('link');
            linkElement.rel = 'stylesheet';
            linkElement.href = 'app/shared/layout/sidebar/sidebar.component.css';
            $document[0].head.appendChild(linkElement);
        };

        vm.loadCss();
        const menus = new AppMenu('main', 'Main Menu', [
            new AppMenuItem('dashboard', 'Dashboard', 'dashboard', 'fa-solid fa-chart-line', [], false, 'admin', true),
            new AppMenuItem('authorization', 'Authorization', '', 'fa-solid fa-user-shield', [
                new AppMenuItem('roles', 'Roles', 'role', 'fa-solid fa-user-tag', [], false, 'admin', true),
                new AppMenuItem('users', 'Users', 'user', 'fa-solid fa-user', [], false, 'admin', true),
            ], true, 'admin', true),
            new AppMenuItem('product', 'Products', '', 'fa-solid fa-box', [
                new AppMenuItem('product', 'Products', 'product', 'fa-solid fa-box', [], false, 'admin', true),
                new AppMenuItem('category', 'Categories', 'category', 'fa-solid fa-tag', [], false, 'admin', true),
                new AppMenuItem('size', 'Sizes', 'size', 'fa-solid fa-ruler-combined', [], false, 'admin', true),
                new AppMenuItem('color', 'Colors', 'color', 'fa-solid fa-palette', [], false, 'admin', true),
            ], true, 'admin', true),
            new AppMenuItem('customer', 'Customers', 'customer', 'fa-solid fa-user-group', [], false, 'admin', true),
            new AppMenuItem('invoice', 'Invoices', 'invoice', 'fa-solid fa-file-invoice-dollar', [], false, 'admin', true),
            new AppMenuItem('warehouseReceipt', 'Warehouse Receipts', 'warehouseReceipt', 'fa-solid fa-warehouse', [], false, 'admin', true),
            new AppMenuItem('supplier', 'Suppliers', 'supplier', 'fa-solid fa-handshake', [], false, 'admin', true),
            new AppMenuItem('paymentMethods', 'Payment Methods', 'paymentMethods', 'fa-solid fa-credit-card', [], false, 'admin', true),
            new AppMenuItem('paymentStatus', 'Payment Status', 'paymentStatus', 'fa-solid fa-check-circle', [], false, 'admin', true),
        ]);


        vm.sidebar = new AppMenu('main', 'Main Menu', menus.items);
    }

})(angular.module('app.layout'));
