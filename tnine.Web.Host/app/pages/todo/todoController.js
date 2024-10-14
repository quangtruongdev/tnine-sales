(function (app) {
    'use strict';

    app.controller('todoController', todoController);

    todoController.$inject = ['$scope', 'baseService', 'toastrService'];

    function todoController($scope, baseService, toastrService) {
        $scope.todos = [];
        $scope.todo = '';
        $scope.addTodo = addTodo;
        $scope.removeTodo = removeTodo;

        activate();

        function activate() {
            // Fetch existing todos from the API
            baseService.get('api/todo').then(function (result) {
                $scope.todos = result.data;
            }).catch(function (error) {
                toastrService.error('Failed to load todos: ' + error.message);
            });
        }

        function addTodo() {
            // Add a new todo item
            baseService.post('api/todo', { todo: $scope.todo }).then(function (result) {
                $scope.todos.push(result.data);
                $scope.todo = ''; // Clear the input field
                toastrService.success('Todo added successfully!');
            }).catch(function (error) {
                toastrService.error('Failed to add todo: ' + error.message);
            });
        }

        function removeTodo(todo) {
            // Remove a todo item
            baseService.remove('api/todo/' + todo.id).then(function () {
                var index = $scope.todos.indexOf(todo);
                if (index !== -1) {
                    $scope.todos.splice(index, 1);
                    toastrService.success('Todo removed successfully!');
                }
            }).catch(function (error) {
                toastrService.error('Failed to remove todo: ' + error.message);
            });
        }
    }
})(angular.module('tnine.todo'));