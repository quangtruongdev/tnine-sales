(function(app) {
    'use strict';

    app.service('notificationService', ['$http', '$q', 'baseService',
        function($http, $q, baseService) {
            this.getNotifications = function() {
                var deferred = $q.defer();
                baseService.get('/api/Notification/GetNotifications')
                    .then(function(result) {
                        deferred.resolve(result);
                    }, function(error) {
                        deferred.reject(error);
                    });
                return deferred.promise;
            };

            this.markAsRead = function(notificationId) {
                var deferred = $q.defer();
                baseService.post('/api/Notification/MarkAsRead', { notificationId: notificationId })
                    .then(function(result) {
                        deferred.resolve(result);
                    }, function(error) {
                        deferred.reject(error);
                    });
                return deferred.promise;
            };
        }
    ]);
})