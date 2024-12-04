(function (app) {
    app.controller('categoryController', categoryController)
        .component('category', {
            templateUrl: '/app/admin/category/category.component.html',
            controller: 'categoryController',
            controllerAs: 'vm'
        });

    categoryController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function categoryController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.categories = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", width: 30 },
                { headerName: "Name", field: "Name" ,width: 100 },
                { headerName: "Description", field: "Description" },
            ],
            rowData: [],
            pagination: true,
            defaultColDef: {
                sortable: true,
                filter: true,
                resizable: true,
                floatingFilter: true,
                flex: 1,
                cellStyle: {
                    display: 'flex',
                    alignItems: 'center',
                }
            },
            rowSelection: 'single',
            enableColResize: true,
            rowHeight: 35,
            frameworkComponents: {
            },
            onGridReady: function (params) {
                vm.gridApi = params.api
                window.addEventListener('resize', () => params.api.sizeColumnsToFit())
            },
            onFirstDataRendered: function (params) {
                params.api.sizeColumnsToFit();
            },
            onRowClicked: function (event) {
                $timeout(() => vm.selectedId = event.data.Id)
            }
        },

        vm.getCategories = function () {
            serviceProxies.categoryService.getAll().then(function (response) {
                vm.categories = response;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.categories);
                }
            }).catch(function (error) {
                console.error('Error fetching categories:', error);
            });
        };

        vm.reloads = function () {
            vm.getCategories();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-category-modal')).controller('createOrEditCategoryModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-category-modal')).controller('createOrEditCategoryModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.categoryService.delete(vm.selectedId).then(function () {
                    vm.getCategories();
                }).catch(function (error) {
                    console.error('Error deleting category:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getCategories();
    }

})(angular.module('app.admin.category'));