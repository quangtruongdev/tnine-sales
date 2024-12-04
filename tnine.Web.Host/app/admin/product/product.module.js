(function () {
	'use strict';
	agGrid.initialiseAgGridWithAngular1(angular);
	angular.module('app.admin.product', [
		'app.services',
		'app.common'
	]);
})();