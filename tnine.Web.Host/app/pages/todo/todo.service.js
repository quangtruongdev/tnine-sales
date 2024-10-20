(function (app) {
    app.service('todoService', todoService);

    todoService.$inject = ['$http'];

    function todoService($http) {
        return {
            getTodos: getTodos,
            createOrUpdate: createOrUpdate,
            deleteTodo: deleteTodo,
            getTodoById: getTodoById,
        };

        function getTodos() {
            return $http.get('/api/todo');
        }

        function createOrUpdate(data) {
            return $http.post('/api/todo', data);
        }

        function getTodoById(id) {
            return $http.get('/api/todo/' + id);
        }

        function deleteTodo(data) {
            return $http.delete('/api/todo/' + data.id);
        }
    }
})(angular.module('tnine.todo'));
