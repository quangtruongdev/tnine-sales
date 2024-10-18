/// <reference path="../../../wwwroot/lib/angular/angular.js" />

(function () {
	angular.module('tnine.todo', [
		'tnine.common',
		//'tnine.ui'
	]).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];
	function config($stateProvider, $urlRouterProvider) {
		$stateProvider
			.state('todo', {
				url: '/todo',
				parent: 'base',
				templateUrl: '/app/pages/todo/todo.component.html',
				controller: 'todoController',
				//resolve: {
				//	loadModule: ['$ocLazyLoad', function ($ocLazyLoad) {
				//		return $ocLazyLoad.load([
				//			'/app/pages/todo/todo.module.js',
                //          '/app/pages/todo/todo.controller.js',
                //          '/app/pages/todo/todo.service.js'
                //         ]);
                //   }]
				//}
			});
		$urlRouterProvider.otherwise('/todo');
	}
})();