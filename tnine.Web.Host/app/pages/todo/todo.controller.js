(function (app) {
    app.controller('todoController', todoController);

    todoController.$inject = ['$scope', 'todoService'];

    function todoController($scope, todoService) {
        $scope.title = 'Todo';
        $scope.todos = [];
        $scope.newTodo = {};

        $scope.columns = [
            { headerName: 'Id', field: 'Id' },
            { headerName: 'Title', field: 'Title' },
        ];

        // get all todos
        todoService.getTodos().then(function (response) {
            $scope.todos = response.data;
        });
    }
})(angular.module('tnine.todo'));
