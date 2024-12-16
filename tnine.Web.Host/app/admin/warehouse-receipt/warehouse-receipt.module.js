(function () {
	'use strict';
	agGrid.initialiseAgGridWithAngular1(angular);
	angular.module('app.admin.warehouse-receipt', [
		'app.services',
		'app.common'
	]);
})();