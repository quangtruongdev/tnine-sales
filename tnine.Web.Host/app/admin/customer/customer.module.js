(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.admin.customer', [
        'app.services',
        'app.common'
    ]);
})();