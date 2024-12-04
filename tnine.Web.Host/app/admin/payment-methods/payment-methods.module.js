(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.pages.paymentMethods', [
        'app.services',
        'app.common'
    ]);
})();