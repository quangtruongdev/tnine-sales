/// <reference path="../../wwwroot/lib/angular/angular.js" />

(function (app) {
    app.factory('baseService', baseService);

    baseService.$inject = ['$http', '$q', '$window', 'toastrService'];

    function baseService($http, $q, $window, toastrService) {
        return {
            get: get,
            post: post,
            put: put,
            remove: remove
        };

        function setHeaders() {
            delete $http.defaults.headers.common['X-Requested-With'];
            if ($window.sessionStorage.getItem('access_token')) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + $window.sessionStorage.getItem('access_token');
                $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            }
        }

        function get(url, params) {
            setHeaders();
            return $http.get(url, params)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (err) {
                    toastrService.error(err.data.message);
                    return $q.reject(err);
                });
        }

        function post(url, data) {
            setHeaders();
            return $http.post(url, data)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (err) {
                    toastrService.error(err.data.message);
                    return $q.reject(err);
                });
        }

        function put(url, data) {
            setHeaders();
            return $http.put(url, data)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (err) {
                    toastrService.error(err.data.message);
                    return $q.reject(err);
                });
        }

        function remove(url) {
            setHeaders();
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                })
                .catch(function (err) {
                    toastrService.error(err.data.message);
                    return $q.reject(err);
                });
        }

    }

})(angular.module('app.services'));