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
        vm.product = {
            Product: {},
            ImgUrl: [],
            ProductVariation: []
        };

        listProductvariation : number = 0;

        vm.listColors = [];
        vm.listSizes = [];
        vm.listCategory = [];

        vm.selectedColors = {};
        vm.selectedSizes = {};

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
                };
                reader.readAsDataURL(file);
            }
        };

        vm.show = function (id) {
            vm.getListColors();
            vm.getListSizes();
            vm.getListCategories();

            if (!id) {
                vm.product = {
                    Product: {},
                    ImgUrl: [],
                    ProductVariation: []
                };
            } else {
                serviceProxies.productService.getProductForEdit(id).then(function (response) {
                    vm.product.Product = response.Product;
                }).catch(function (error) {
                    console.error('Error fetching product:', error);
                });

                serviceProxies.productService.getListImages(id).then(function (response) {
                    vm.product.ImgUrl = response.Results;
                }).catch(function (error) {
                    console.error('Error fetching product images:', error);
                });
                serviceProxies.productVariationService.getForEdit(id).then(function (response) {
                    vm.product.ProductVariation = response;
                }).catch(function (error) {
                    console.error('Error fetching product variations:', error);
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

        vm.addProductVariation = function () {
            if (!Array.isArray(vm.product.ProductVariation)) {
                vm.product.ProductVariation = []; 
            }

            vm.product.ProductVariation.push({
                ColorId: null,
                SizeId: null,
                Quantity: null
            });
        };

        vm.removeImage = function (index) {
            if (!Array.isArray(vm.product.ImgUrl)) {
                vm.product.ImgUrl = [];
            }
            if (confirm("Bạn có chắc chắn muốn xóa không"))
            {
                vm.product.ImgUrl.splice(index, 1);
                if (vm.product.ImgUrl.Id != null) {
                    vm.deleteImage(vm.product.ImgUrl.Id);
                }
            }
        };

        vm.removeProductVariation = function (variation) {
            if (confirm("Bạn có chắc chắn muốn xóa không")) {
                if (variation.ColorId != null) {
                    vm.deleteProductVariation(variation);
                }

                var index = vm.product.ProductVariation.indexOf(variation);
                if (index > -1) {
                    vm.product.ProductVariation.splice(index, 1);
                }
            }
        };

        vm.deleteProductVariation = function (data) {
            serviceProxies.productVariationService.delete(data).then(function () {
                console.log('Deleted product variation');
            }).catch(function (error) {
                console.error('Error deleting product variation:', error);
            });
        };

    }

})(angular.module('app.admin.product'));
