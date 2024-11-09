(function (app) {
    app.controller('categoryController', categoryController)
        .component('category', {
            templateUrl: '/app/admin/Category/category.component.html',
            controller: 'categoryController',
            controllerAs: 'vm'
        });

    categoryController.$inject = ['serviceProxies', '$state', '$element'];

    function categoryController(serviceProxies, $state, $element) {
        var vm = this;
        vm.categories = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" },
            { headerName: "Description", field: "Description" },
        ];

        vm.getCategories = function () {
            serviceProxies.categoryService.getAll().then(function (response) {
                vm.categories = response;
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