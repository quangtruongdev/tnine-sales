/// <reference path="../../wwwroot/lib/angular/angular.js" />

const moment = require("../../wwwroot/lib/moment/moment");

(function () {
    angular.module('app.services').factory('dataFormatService', function () {
        return {
            dateFormat: function (date) {
                if (moment(date).isValid()) {
                    return moment(date).format('YYYY-MM-DD');
                } else {
                    return 'Invalid date';
                }
            },
        };
    });
})();