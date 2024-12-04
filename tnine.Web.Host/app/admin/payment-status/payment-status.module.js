(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.pages.paymentStatus', [
        'app.services',
        'app.common'
    ]);
})();