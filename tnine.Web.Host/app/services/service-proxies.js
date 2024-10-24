(function (app) {
    app.factory('serviceProxies', serviceProxies);

    serviceProxies.$inject = ['$http', '$q'];

    function serviceProxies($http, $q) {
        //var serviceBase = 'http://localhost:44332/';
        var serviceProxies = {};

        function getToken() {
            return sessionStorage.getItem('token');
        }

        function httpConfig(contentType = 'application/json') {
            var token = getToken();
            var config = {
                headers: {
                    'Content-Type': contentType,
                    'Authorization': 'Bearer ' + token,
                }
            };
            return config;
        }

        serviceProxies['apiTokenAuth'] = function (data) {
            return $http({
                url: serviceBase + 'TokenAuth/Authenticate',
                method: 'POST',
                data: data,
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (response) {
                sessionStorage.setItem('token', response.data.result.accessToken);
                return response.data;
            });
        };

        serviceProxies.todoService = {
            getTodos: function () {
                return $http.get('api/todo');
            },
            getTodoById: function (id) {
                return $http.get('api/todo/' + id);
            },
            createOrUpdate: function (data) {
                return $http.post('api/todo', data);
            },
            deleteTodo: function (id) {
                return $http.delete('api/todo/' + id);
            }
        };

        serviceProxies['apiUpload'] = function (data) {
            var formData = new FormData();

            formData.append('file', data);

            return $http.post(serviceBase + 'upload', formData, {
                transformRequest: angular.identity,
                headers: {
                    'Content-Type': undefined
                }
            });
        };

        return serviceProxies;
    }

})(angular.module('tnine.services'));