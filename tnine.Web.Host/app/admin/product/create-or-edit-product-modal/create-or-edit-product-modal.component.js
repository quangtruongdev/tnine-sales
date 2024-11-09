(function (app) {
    app.controller('CreateOrEditProductModalController', CreateOrEditProductModalController)
        .component('createOrEditProductModal', {
            templateUrl: '/app/admin/product/create-or-edit-product-modal/create-or-edit-product-modal.component.html',
            controller: 'CreateOrEditProductModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditProductModalController.$inject = ['serviceProxies'];

    function CreateOrEditProductModalController(serviceProxies, $element) {
        var vm = this;
        vm.saving = false;
        vm.product =
        {
            Product: {},
            ImgUrl : []
        };

        vm.listColors = {};
        vm.listSizes = {};

        vm.saving = false;
        vm.listCategory = {};
        vm.getListCategories = function () {
            serviceProxies.categoryService.getAll().then(function (response) {
                vm.listCategory = response;
            }).catch(function (error) {
                console.error('Error fetching category:', error);
            });
        };

        vm.getListColors = function () {
            serviceProxies.productService.getListColors().then(function (response) {
                vm.listColors = response;
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.getListSizes = function () {
            serviceProxies.productService.getListSizes().then(function (response) {
                vm.listSizes = response;
            }).catch(function (error) {
                console.error('Error fetching sizes:', error);
            });
        };

        vm.uploadImages = function (event) {
            var files = event.target.files;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var reader = new FileReader();
                reader.onload = function (event) {
                    if (!Array.isArray(vm.product.ImgUrl)) {
                        vm.product.ImgUrl = [];
                    }

                    vm.product.ImgUrl.push({
                        ImgUrl: event.target.result
                    });
                    console.log(vm.product.ImgUrl);
                };
                reader.readAsDataURL(file);
            }
        };



        vm.removeImage = function (index) {
            vm.product.ImgUrl.splice(index, 1);
        };

        vm.show = function (id) {
            vm.getListCategories();
            if (!id) {
                vm.product = {
                    Product: {},
                    ImgUrl: {},
                    ColorIds: [],
                    SizeIds: []
                };
            } else {
                serviceProxies.productService.getProductForEdit(id).then(function (response) {
                    vm.product.Product = response.Product;
                }).catch(function (error) {
                    console.error('Error fetching product:', error);
                });

                serviceProxies.productService.getListImages(id).then(function (response) {
                    vm.product.ImgUrl = response;
                }).catch(function (error) {
                    console.error('Error fetching product images:', error);
                });
            }
            $('#createOrEditProductModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            serviceProxies.productService.createOrEdit(vm.product).then(function () {
                vm.close();
                vm.onSaved();
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

})(angular.module('app.admin.product'));