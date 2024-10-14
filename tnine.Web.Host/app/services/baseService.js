/// <reference path="../../wwwroot/lib/angular/angular.js" />

(function (app) {
    app.factory('baseService', baseService);

    baseService.$inject = ['$http', 'toastrService'];

    const headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    };

    function baseService($http, toastrService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        };

        function get(url, params, success, error) {
            $http.get(url, { headers: headers, params: params })
                .then(success)
                .catch(function (err) {
                    handleError(err, error);
                });
        }

        function post(url, data, success, error) {
            $http.post(url, data, { headers: headers })
                .then(success)
                .catch(function (err) {
                    handleError(err, error);
                });
        }

        function put(url, data, success, error) {
            $http.put(url, data, { headers: headers })
                .then(success)
                .catch(function (err) {
                    handleError(err, error);
                });
        }

        function del(url, success, error) {
            $http.delete(url, { headers: headers })
                .then(success)
                .catch(function (err) {
                    handleError(err, error);
                });
        }

        function handleError(error, errorCallBack) {
            toastrService.error(error.data.error || 'An error occurred', 'Error');
            if(errorCallback) errorCallback(error);
        }
    }
})(angular.module('tnine.services'));