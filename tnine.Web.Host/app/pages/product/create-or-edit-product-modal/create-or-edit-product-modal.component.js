(function (app) {
    app.controller('CreateOrEditProductModalController', CreateOrEditProductModalController)
        .component('createOrEditProductModal', {
            templateUrl: '/app/pages/product/create-or-edit-product-modal/create-or-edit-product-modal.component.html',
            controller: 'CreateOrEditProductModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditProductModalController.$inject = ['serviceProxies'];

    function CreateOrEditProductModalController(serviceProxies, $element) {
        var vm = this;
        vm.product = {};
        //vm.product =
        //{
        //    Product= {},
        //    ImgUrl = {},
        //};
        vm.saving = false;
        listCategory = {};
        //vm.getListCategories() = function () {
        //    serviceProxies.categoryService.getList().then(function (response) {
        //        vm.listCategory = response
        //    }).catch(function (error) {
        //        console.error('Error fetching category:', error);
        //    });
        //}

        vm.show = function (id) {
           
            if (!id) {
                vm.product = {};
            } else {
                serviceProxies.productService.getProductForEdit(id).then(function (response) {
                    vm.product.Product = response.Product;
                }).catch(function (error) {
                    console.error('Error fetching product:', error);
                });
            }
            $('#createOrEditProductModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.productService.createOrEdit(vm.product).then(function () {
                vm.close();
                location.reload();
            }).catch(function (error) {
                console.error('Error saving product:', error);
            }).finally(function () {
                vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditProductModal').modal('hide');
        };
    }

})(angular.module('app.pages.product'));