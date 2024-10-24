(function (app) {
    app.controller('todoController', todoController)
        .component('todo', {
            templateUrl: '/app/pages/todo/todo.component.html',
            controller: todoController,
            controllerAs: 'vm',
        });

    todoController.$inject = ['$scope', 'serviceProxies','$state', '$element'];

    function todoController($scope, serviceProxies, $state, $element) {
        var vm = this;
        vm.todos = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Title", field: "Title" },
            { headerName: "Description", field: "Description" },
        ];

        vm.getTodos = function () {
            serviceProxies.todoService.getTodos().then(function (response) {
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
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.todoService.deleteTodo(id).then(function (response) {
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
