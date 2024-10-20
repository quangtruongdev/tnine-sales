(function (app) {
    app.controller('todoController', todoController)
        .component('todo', {
            templateUrl: '/app/pages/todo/todo.component.html',
            controller: todoController,
            controllerAs: 'vm',
        });

    todoController.$inject = ['$scope', 'todoService', '$state', '$element'];

    function todoController($scope, todoService, $state, $element) {
        var vm = this;
        vm.todos = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Title", field: "Title" },
            { headerName: "Description", field: "Description" },
        ];

        vm.getTodos = function () {
            todoService.getTodos().then(function (response) {
                vm.todos = response.data;
            });
        }

        vm.reloads = function () {
            vm.getTodos();
        }

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-todo-modal')).controller('createOrEditTodoModal').show();
        };


        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-todo-modal')).controller('createOrEditTodoModal').show(vm.selectedId);
        }

        vm.delete = function (id) {
            //todoService.delete(id).then(function (response) {
            //    vm.getTodos();
            //});

            // are you sure?
            if (confirm('Are you sure you want to delete?')) {
                todoService.delete(id).then(function (response) {
                    vm.getTodos();
                });
            }
        }

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getTodos();
    }

})(angular.module('tnine.todo'));
