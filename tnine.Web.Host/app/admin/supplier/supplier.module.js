(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.admin.supplier', [
        'app.services',
        'app.common'
    ]);
})();