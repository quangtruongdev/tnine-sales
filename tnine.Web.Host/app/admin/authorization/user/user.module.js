(function () {
    'use strict';
    agGrid.initialiseAgGridWithAngular1(angular);
    angular.module('app.admin.authorization.user', [
        'app.services',
        'app.common'
    ]);
})();