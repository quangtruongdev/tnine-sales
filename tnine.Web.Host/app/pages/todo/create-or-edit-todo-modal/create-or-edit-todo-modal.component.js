(function (app) {
    app.controller('createOrEditTodoController', createOrEditTodoController)
        .component('createOrEditTodoModal', {
            templateUrl: '/app/pages/todo/create-or-edit-todo-modal/create-or-edit-todo-modal.component.html',
            controller: createOrEditTodoController,
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    createOrEditTodoController.$inject = ['serviceProxies'];

    function createOrEditTodoController(serviceProxies) {
        var vm = this;
        vm.todo = {};
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.todo = {};
            } else {
                serviceProxies.todoService.getTodoById(id).then(function (response) {
                    vm.todo = response.data.Todo;
                }).catch(function (error) {
                    console.error('Error fetching todo:', error);
                });
            }
            $('#createOrEditTodoModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.todoService.createOrEdit(vm.todo).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving todo:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditTodoModal').modal('hide');
        };
    }
})(angular.module('tnine.todo'));