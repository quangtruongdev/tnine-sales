(function (app) {
    app.directive('pagination', function () {
        return {
            restrict: 'E',
            scope: {
                currentPage: '=',
                totalPages: '=',
                onPageChanged: '&'
            },
            templateUrl: '/app/shared/common/pagination/pagination.component.html',
            controller: function ($scope) {
                $scope.pages = [];

                $scope.$watch('totalPages', function () {
                    $scope.pages = [];
                    for (var i = 1; i <= $scope.totalPages; i++) {
                        $scope.pages.push(i);
                    }
                });

                $scope.changePage = function (page) {
                    if (page < 1 || page > $scope.totalPages) {
                        return;
                    }

                    if ($scope.currentPage === page) {
                        return;
                    }

                    $scope.currentPage = page;
                    $scope.onPageChanged({ page: page });
                };
            }
        };
    });
})(angular.module('app.common'));
