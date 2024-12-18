angular.module('app.layout').service('AppMenuItem', function () {
    function AppMenuItem(name, displayName, route, icon, items, external, roleName, requiresAuth) {
        this.name = name || '';
        this.displayName = displayName || '';
        this.route = route || '';
        this.icon = icon || '';
        this.items = items || [];
        this.external = external || false;
        this.roleName = roleName || '';
        this.requiresAuth = requiresAuth !== undefined ? roleName : false;
    }

    return AppMenuItem;
});
