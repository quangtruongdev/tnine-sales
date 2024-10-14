(function (app) {
    'use strict';

    app.servie('authService', ['$http', '$q', 'baseService',
        function ($http, $q, baseService) {
            var user = null;
            var token = null;

            var authentication = {
                isAuth: false,
                userName: ""
            };

            this.setToken = function (token) {
                token = token;
                localStorage.setItem('token', token);
            };


            this.login = function (username, password) {
                var deferred = $q.defer();
                baseService.post('/api/Account/Login', { username: username, password: password })
                    .then(function (result) {
                        user = result;
                        deferred.resolve(result);
                    }, function (error) {
                        deferred.reject(error);
                    });
                return deferred.promise;
            };
        }]);
})(angular.module('tnine.services'));