(function (app) {
    app.factory('baseService', baseService);

    baseService.$inject = ['$http', '$q', 'toastrService'];

    function baseService($http, $q, toastrService) {
        function get(url, params) {
            return $http.get(url, { params: params })
                .then(function (response) {
                    return response.data;
                })
                .catch(function (error) {
                    toastrService.error(error.data.Message);
                    return $q.reject(error);
                });
        }

        function post(url, data) {
            return $http.post(url, data)
                .then(function (response) {
                    toastrService.success(response.data.Message);
                    return response.data;
                })
                .catch(function (error) {
                    toastrService.error(error.data.Message);
                    return $q.reject(error);
                });
        }

        function put(url, data) {
            return $http.put(url, data)
                .then(function (response) {
                    toastrService.success(response.data.Message);
                    return response.data;
                })
                .catch(function (error) {
                    toastrService.error(error.data.Message);
                    return $q.reject(error);
                });
        }

        function remove(url) {
            return $http.delete(url)
                .then(function (response) {
                    toastrService.success(response.data.Message);
                    return response.data;
                })
                .catch(function (error) {
                    toastrService.error(error.data.Message);
                    return $q.reject(error);
                });
        }

        return {
            get: get,
            post: post,
            put: put,
            remove: remove
        };
    }
})(angular.module('app.services'));