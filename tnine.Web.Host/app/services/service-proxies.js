(function (app) {
    app.factory('serviceProxies', serviceProxies);

    serviceProxies.$inject = ['$http', '$q', '$injector', 'baseService'];

    function serviceProxies($http, $q, $injector, baseService) {
        //var serviceBase = 'http://localhost:44332/';
        var serviceProxies = {};

        var tokenInfo = {
            accessToken: sessionStorage.getItem('access_token')
        };

        this.setHeaders = function () {
            delete $http.defaults.headers.common['X-Requested-With'];
            if (tokenInfo) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            }
        }

        serviceProxies.accountService = {
            login: function (data) {
                var deferred = $q.defer();
                var requestData = "grant_type=password&username=" + encodeURIComponent(data.username) + "&password=" + encodeURIComponent(data.password);

                $http.post('oauth/token', requestData, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                })
                    .then(function (response) {
                        sessionStorage.setItem('access_token', response.data.access_token);
                        tokenInfo.accessToken = response.data.access_token;
                        return getUserInfo(response.data.access_token);
                    })
                    .then(function (userInfoResponse) {
                        deferred.resolve(userInfoResponse);
                    })
                    .catch(function (err) {
                        console.error(err);
                        deferred.reject(err);
                    });

                return deferred.promise;
            },
            logout: function () {
                sessionStorage.removeItem('access_token');
                sessionStorage.removeItem('userInfo');
            },
        };

        function getUserInfo(token) {
            return $http.get('api/user/GetUserInfo', {
                headers: { 'Authorization': 'Bearer ' + token }
            }).then(function (response) {
                sessionStorage.setItem('userInfo', JSON.stringify(response.data));
            });
        }

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

        serviceProxies.roleService = {
            getAll: function () {
                return baseService.get('api/role');
            },
            getById: function (id) {
                return baseService.get('api/role/' + id);
            },
            createOrUpdate: function (data) {
                return baseService.post('api/role', data);
            },
            delete: function (id) {
                return baseService.delete('api/role/' + id);
            },
            hasRole: function (roleName) {
                var userInfo = JSON.parse(sessionStorage.getItem('userInfo'));
                return userInfo && userInfo.role && userInfo.role.includes(roleName);
            }
        };

        serviceProxies.permissionService = {
            getAll: function () {
                return baseService.get('api/permission');
            },
            getById: function (id) {
                return baseService.get('api/permission/' + id);
            },
            createOrUpdate: function (data) {
                return baseService.post('api/permission', data);
            },
            delete: function (id) {
                return baseService.delete('api/permission/' + id);
            }
        };

        serviceProxies.userService = {
            getAll: function () {
                return baseService.get('api/user');
            },
            getById: function (id) {
                return baseService.get('api/user/' + id);
            },
            createOrUpdate: function (data) {
                return baseService.post('api/user', data);
            },
            delete: function (id) {
                return baseService.delete('api/user/' + id);
            }
        };

        serviceProxies.customerService = {
            getAll: function () {
                return baseService.get('api/customer');
            },
            getCustomerForEdit: function (id) {
                return baseService.get('api/customer/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/customer', data);
            },
            delete: function (id) {
                return baseService.remove('api/customer/' + id);
            }
        };

        serviceProxies.productService = {
            getAll: function () {
                return baseService.get('api/product');
            },
            getProductForEdit: function (id) {
                return baseService.get('api/product/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/product', data);
            },
            delete: function (id) {
                return baseService.remove('api/product/' + id);
            }
        };

        serviceProxies.categoryService = {
            getList: function () {
                return baseService.get('api/product/category');
            },
        };
        

        return serviceProxies;
    }

})(angular.module('app.services'));