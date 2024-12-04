(function () {
	'use strict';
	agGrid.initialiseAgGridWithAngular1(angular);
	angular.module('app.admin.category', [
		'app.services',
		'app.common'
	]);
})();