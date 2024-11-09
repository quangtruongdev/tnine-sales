/// <reference path="../../../../wwwroot/lib/angular/angular.js" />

(function (app) {
    'use strict';

    angular
        .module('app.common', [])
        .directive('tnineTable', tnineTable);

    function tnineTable() {
        return {
            restrict: 'E',
            templateUrl: '/app/shared/common/table/table.component.html',
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

    tableController.$inject = ['$scope', '$sce'];

    function tableController($scope, $sce) {
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

        vm.formatData = function (item, column) {
            if (column.field === 'HexCode') {
                return $sce.trustAsHtml(`<div class="d-flex align-items-center justify-content-between" style="width: 150px"><span>${item[column.field]}</span> 
                    <span style="display:inline-block; width:20px; height:20px; background-color:${item[column.field]};"></span></div>`);
            }

            if (column.field === 'ImgUrl') {
                return $sce.trustAsHtml(`<img src="${item[column.field]}" style="width: 40px; height: 40px;" />`);
            }
            return item[column.field] ? $sce.trustAsHtml(`<span>${item[column.field]}</span>`) : '';
        };

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