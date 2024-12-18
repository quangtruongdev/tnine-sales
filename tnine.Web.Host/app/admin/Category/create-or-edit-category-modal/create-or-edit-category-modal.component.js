(function (app) {
    app.controller('CreateOrEditCategoryModalController', CreateOrEditCategoryModalController)
        .component('createOrEditCategoryModal', {
            templateUrl: '/app/admin/category/create-or-edit-category-modal/create-or-edit-category-modal.component.html',
            controller: 'CreateOrEditCategoryModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditCategoryModalController.$inject = ['serviceProxies'];

    function CreateOrEditCategoryModalController(serviceProxies) {
        var vm = this;
        vm.category = {};
        vm.saving = false;
        vm.listCategories = {};

        vm.getCategories = function () {
            serviceProxies.categoryService.getAll().then(function (response) {
                vm.listCategories = response;
            }).catch(function (error) {
                console.error('Error fetching categories:', error);
            });
        };

        vm.show = function (id) {
            vm.getCategories();
            if (!id) {
                vm.category = {};
            } else {
                serviceProxies.categoryService.getCategoryForEdit(id).then(function (response) {
                    vm.category = response.Category;
                }).catch(function (error) {
                    console.error('Error fetching category:', error);
                });
            }
            $('#createOrEditCategoryModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.categoryService.createOrEdit(vm.category).then(function () {
                vm.onSaved();
                vm.close();
            }).catch(function (error) {
                console.error('Error saving category:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditCategoryModal').modal('hide');
        };

        vm.isSaveDisabled = function () {
            if (!vm.category.Name) {
                return true;
            }

            return false;
        };
    }

})(angular.module('app.admin.category'));