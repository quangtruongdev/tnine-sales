(function (app) {
    app.controller('todoController', todoController)
        .component('todo', {
            templateUrl: '/app/pages/todo/todo.component.html',
            controller: todoController,
            controllerAs: 'vm',
        });

    todoController.$inject = ['serviceProxies'];

    function todoController(serviceProxies) {
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
            }).catch(function (error) {
                console.error('Error fetching todos:', error);
            });
        };

        vm.reloads = function () {
            vm.getTodos();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-todo-modal')).controller('createOrEditTodoModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-todo-modal')).controller('createOrEditTodoModal').show(vm.selectedId);
        };

        vm.delete = function (id) {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.todoService.deleteTodo(id).then(function () {
                    vm.getTodos();
                }).catch(function (error) {
                    console.error('Error deleting todo:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getTodos();
    }

})(angular.module('tnine.todo'));