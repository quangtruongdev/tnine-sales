(function (app) {
    app.factory('authService', authService);

    authService.$inject = [];

    function authService($http, $q, $injector) {
        return {
            isAuthenticated: function() {
                if(sessionStorage.getItem('access_token')) {
                    return true;
                }
                return false;
            },
            getUserWithRole: function () {
                var userInfo = JSON.parse(sessionStorage.getItem('userInfo'));
                if(userInfo) {
                    return userInfo.role;
                }
                return null;
            }
        }
    }
})(angular.module('app.services'));