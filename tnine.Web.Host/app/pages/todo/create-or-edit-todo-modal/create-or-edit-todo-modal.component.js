(function (app) {
    app.controller('createOrEditTodoController', createOrEditTodoController)
        .component('createOrEditTodoModal', {
            templateUrl: '/app/pages/todo/create-or-edit-todo-modal/create-or-edit-todo-modal.component.html',
            controller: createOrEditTodoController,
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            }
        });

    createOrEditTodoController.$inject = ['$scope', 'todoService', '$stateParams', '$state'];

    function createOrEditTodoController($scope, todoService, $stateParams, $state) {
        var vm = this;
        vm.message = 'Test';
        vm.todo = {};  
        vm.saving = false;

        vm.show = function (id) {
            if (!id) {
                vm.todo = {};
                $('#createOrEditTodoModal').modal('show');
            } else {
                todoService.getTodoById(id).then(function (response) {
                    vm.todo = response.data.Todo;
                    $('#createOrEditTodoModal').modal('show'); 
                }).catch(function (error) {
                    console.error('Error fetching todo:', error);
                });
            }
        };

        vm.save = function () {
            vm.saving = true;
            todoService.createOrUpdate(vm.todo).then(function (response) {
                vm.saving = false;
                vm.close();
                vm.onSaved();
            }).catch(function (error) {
                console.error('Error saving todo:', error);
            });
        };

        vm.close = function () {
            $('#createOrEditTodoModal').modal('hide');
        };
    }
})(angular.module('tnine.todo'));