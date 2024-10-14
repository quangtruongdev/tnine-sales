/// <reference path="../../../wwwroot/lib/angular/angular.js" />

(function () {
	'use strict';

	angular.module('tnine.todo', [
		'tnine.common',
	]).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider
			.state('todo', {
				url: '/todo',
				parent: 'base',
				templateUrl: 'app/pages/todo/todo.html',
				controller: 'todoController',
			});
		$urlRouterProvider.otherwise('/todo');
	}
})();