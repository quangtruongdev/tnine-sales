angular.module('app.layout').service('AppMenu', ['serviceProxies', function (serviceProxies) {
    function AppMenu(name, displayName, items) {
        this.name = name || '';
        this.displayName = displayName || '';

        if (!serviceProxies || !serviceProxies.roleService) {
            throw new Error('serviceProxies or roleService is undefined');
        }

        //this.items = items.filter(e => e.roleName === null || serviceProxies.roleService.hasRole(e.roleName));
        this.items = items.filter(e => serviceProxies.roleService.hasRole(e.roleName));
        //this.items = items;
    };

    return AppMenu;
}]);