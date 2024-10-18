/// <reference path="../../../../wwwroot/lib/angular/angular.js" />

(function (app) {
    'use strict';

    angular
        .module('tnine.ui.table', [])
        .directive('tnineTable', tnineTable);

    console.log('tnine.ui.table module loaded');

    function tnineTable() {
        return {
            restrict: 'E',
            templateUrl: '/app/components/ui/table/table.component.html',
            controller: tableController,
            controllerAs: 'vm',
            scope: {
                data: '<',
                columns: '<',
                options: '<',
                onRowClick: '&',
                totalCount: '<',
                totalPages: '<',
                pageIndex: '<',
                pageSize: '<'
            }
        };
    }

    tableController.$inject = ['$scope'];

    function tableController($scope) {
        var vm = this;
        vm.data = [];
        vm.columns = $scope.columns;
        vm.options = $scope.options;
        vm.onRowClick = $scope.onRowClick;
        vm.totalCount = $scope.totalCount;
        vm.totalPages = $scope.totalPages;
        vm.pageIndex = $scope.pageIndex;    
        vm.pageSize = $scope.pageSize;


        $scope.$watch('data', function (newVal, oldVal) {
            vm.data = newVal;
        });

        vm.rowClicked = function (row) {
            if (vm.onRowClick) {
                vm.onRowClick({ row: row });
            }
        }

        vm.sort = function (column) {
            if (column.sortable) {
                if (column.sortDirection === 'asc') {
                    column.sortDirection = 'desc';
                } else {
                    column.sortDirection = 'asc';
                }
            }
        }

        vm.next = function () {
            if (vm.pageIndex < vm.totalPages) {
                vm.pageIndex++;
            }
        }

        vm.prev = function () {
            if (vm.pageIndex > 1) {
                vm.pageIndex--;
            }
        }
    }

})();