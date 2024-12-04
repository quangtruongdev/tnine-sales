(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.admin.color', [
        'app.services',
        'app.common'
    ]);
})();