(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.admin.authorization.role', [
        'app.services',
        'app.common'
    ]);
})();