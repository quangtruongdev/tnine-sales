angular.module('app.layout').service('AppMenuItem', function () {
    function AppMenuItem(name, displayName, route, icon, items, external, permissionName, requiresAuth) {
        this.name = name || '';
        this.displayName = displayName || '';
        this.route = route || '';
        this.icon = icon || '';
        this.items = items || [];
        this.external = external || false;
        this.permissionName = permissionName || '';
        this.requiresAuth = requiresAuth !== undefined ? requiresAuth : !!this.permissionName;
    }

    return AppMenuItem;
});
