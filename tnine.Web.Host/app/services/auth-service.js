(function (app) {
    app.factory('authService', authService);

    authService.$inject = ['$http', '$q', '$injector'];

    function authService($http, $q, $injector) {
        var isFetching = false; // Biến cờ để kiểm soát việc gọi API
        var lastFetchedTime = null; // Lưu thời gian lấy thông tin gần nhất

        return {
            getUserInfo: function () {
                var deferred = $q.defer();

                // Nếu đang gọi API, trả về promise hiện tại
                if (isFetching) {
                    return deferred.promise;
                }

                isFetching = true; // Đánh dấu là đang gọi API

                $http.get('api/account/getAccountInfo')
                    .then(function (response) {
                        sessionStorage.setItem('accountInfo', JSON.stringify(response.data));
                        lastFetchedTime = new Date(); // Cập nhật thời gian lấy thông tin
                        isFetching = false; // Đánh dấu là đã xong
                        deferred.resolve(response.data);
                    })
                    .catch(function (error) {
                        isFetching = false; // Đánh dấu là đã xong
                        var state = $injector.get('$state');
                        state.go('login');
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            isAuthenticated: function () {
                var userInfo = JSON.parse(sessionStorage.getItem('accountInfo'));
                return userInfo && userInfo.IsAuthenticated ? userInfo.IsAuthenticated : false;
            },
            initialize: function () {
                if (!this.isAuthenticated()) {
                    return this.getUserInfo(); // Gọi getUserInfo nếu chưa có thông tin
                }

                // Kiểm tra thời gian lấy thông tin gần nhất (ví dụ: 5 phút)
                var now = new Date();
                var timeSinceLastFetch = (now - lastFetchedTime) / 1000; // Tính thời gian tính bằng giây

                if (!lastFetchedTime || timeSinceLastFetch > 300) { // 300 giây = 5 phút
                    return this.getUserInfo(); // Gọi lại API nếu đã quá hạn
                }

                return $q.resolve(JSON.parse(sessionStorage.getItem('accountInfo')));
            }
        };
    }
})(angular.module('app.services'));