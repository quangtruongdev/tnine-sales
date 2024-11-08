angular.module('app.layout').service('AppMenu', ['serviceProxies', function (serviceProxies) {
    function AppMenu(name, displayName, items) {
        this.name = name || '';
        this.displayName = displayName || '';

        //this.items = items.filter(e => e.permissionName === null || serviceProxies.permissionService.isGranted(e.permissionName));
        this.items = items;
    };

    return AppMenu;
}]);