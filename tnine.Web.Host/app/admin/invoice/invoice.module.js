(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.admin.invoice', [
        'app.services',
        'app.common'
    ]);
})();