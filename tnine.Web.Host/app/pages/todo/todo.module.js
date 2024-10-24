/// <reference path="../../../wwwroot/lib/angular/angular.js" />

(function () {
	angular.module('tnine.todo', [
		'tnine.services'
	]).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];
	function config($stateProvider, $urlRouterProvider) {
		$stateProvider
			.state('todo', {
				url: '/todo',
				parent: 'base',
				templateUrl: '/app/pages/todo/todo.component.html',
				controller: 'todoController',
				controllerAs: 'vm', 
			});
		$urlRouterProvider.otherwise('/todo');
	};
})();