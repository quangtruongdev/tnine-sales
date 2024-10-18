/// <reference path="../../wwwroot/lib/angular/angular.js" />

(function (app) {
    app.factory('baseService', baseService);

    baseService.$inject = ['$http'];

    const headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    };

    function baseService($http) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        };

    function get(url) {
        return $http.get(url, { headers: headers });
    }

    }
})(angular.module('tnine.services'));