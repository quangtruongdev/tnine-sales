(function (app) {
    app.service('todoService', todoService);

    todoService.$inject = ['$http'];

    function todoService($http) {
        return {
            getTodos: getTodos,
            addTodo: addTodo,
            updateTodo: updateTodo,
            deleteTodo: deleteTodo
        };

        function getTodos() {
            return $http.get('/api/todo');
        }

        function addTodo(todo) {
            return $http.post('/api/todo', todo);
        }

        function updateTodo(todo) {
            return $http.put('/api/todo/' + todo.id, todo);
        }

        function deleteTodo(todo) {
            return $http.delete('/api/todo/' + todo.id);
        }
    }
})(angular.module('tnine.todo'));
