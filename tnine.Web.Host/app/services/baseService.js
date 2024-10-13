/// <reference path="../../wwwroot/lib/angular/angular.js" />

(function (app) {
    app.factory('baseService', baseService);

    baseService.$inject = ['$http', 'toastrService', 'authService'];

    var baseUrl = 'http://localhost:5000';
    var http;
    var toastr;


})