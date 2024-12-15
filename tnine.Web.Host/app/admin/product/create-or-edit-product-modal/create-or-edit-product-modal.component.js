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

    CreateOrEditProductModalController.$inject = ['serviceProxies', '$http', '$element'];

    function CreateOrEditProductModalController(serviceProxies, $http, $element) {
        var vm = this;
        vm.saving = false;
        vm.product = {};
        vm.ProductVariation = [];
        vm.ImgUrl = [];

        listProductvariation : number = 0;

        vm.listColors = [];
        vm.listSizes = [];
        vm.listCategory = [];

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
            var formData = new FormData();

            for (var i = 0; i < files.length; i++) {
                formData.append('file' + i, files[i]);
            }

            $http.post('api/image/upload', formData, {
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).then(function (response) {
                if (!Array.isArray(vm.ImgUrl)) {
                    vm.ImgUrl = [];
                }
                vm.ImgUrl.push({
                    ImgUrl: response.data.imageUrl
                });
            }).catch(function (error) {
                console.error('Error uploading image:', error);
            });
        };


        vm.show = function (id) {
            vm.getListColors();
            vm.getListSizes();
            vm.getListCategories();

            if (!id) {
                vm.product = {};
                vm.ProductVariation = [];
                vm.ImgUrl = [];
            } else {
                vm.ProductVariation = [];
                vm.ImgUrl = [];
                serviceProxies.productService.getProductForEdit(id).then(function (response) {
                    vm.product = response.Product;
                }).catch(function (error) {
                    console.error('Error fetching product:', error);
                });

                serviceProxies.productService.getListImages(id).then(function (response) {
                    vm.ImgUrl = response.Results;
                }).catch(function (error) {
                    console.error('Error fetching product images:', error);
                });
                serviceProxies.productVariationService.getForEdit(id).then(function (response) {
                    vm.ProductVariation = response;
                }).catch(function (error) {
                    console.error('Error fetching product variations:', error);
                });
            }
            $('#createOrEditProductModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            vm.productId = 0;
            if (vm.product.Price <= 0) {
                alert("Giá sản phẩm không được nhỏ hơn 0");
                vm.saving = false;
                return;
            }
            if (vm.ProductVariation && vm.ProductVariation.some(variation => variation.Quantity < 0)) {
                alert("Số lượng sản phẩm không được nhỏ hơn 0");
                vm.saving = false;
                return;
            }
            serviceProxies.productService.createOrEdit(vm.product).then(function (response) {
                vm.productId = response;

                vm.ProductVariation.forEach(function (variation) {
                    variation.ProductId = vm.productId;
                });
                vm.ImgUrl = vm.ImgUrl.filter(function (image) {
                    if (!image.Id) {
                        image.ProductId = vm.productId;
                        return true;
                    }
                    return false;
                });
                //vm.ImgUrl.forEach(function (image) {
                //    image.ProductId = vm.productId;
                //});
                
                serviceProxies.productVariationService.createOrEdit(vm.ProductVariation).then(function () {

                }).catch(function (error) {
                    console.error('Error saving product:', error);
                });

                serviceProxies.imageService.createOrEdit(vm.ImgUrl).then(function () {
                    vm.onSaved();
                    vm.close();
                }).catch(function (error) {
                    console.error('Error saving product:', error);
                });
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
            if (!Array.isArray(vm.ProductVariation)) {
                vm.ProductVariation = []; 
            }

            vm.ProductVariation.push({
                ColorId: null,
                SizeId: null,
                Quantity: null
            });
        };

        vm.removeImage = function (img) {
            
            if (confirm("Bạn có chắc chắn muốn xóa không"))
            {
                vm.ImgUrl = vm.ImgUrl.filter(function (item) {
                    return item !== img;
                });

                if(img.Id != null)
                {
                    vm.deleteImage(img.Id);
                };
            }
        };

        vm.deleteImage = function (id) {
            serviceProxies.imageService.delete(id).then(function () {
                console.log('Deleted image');
            }).catch(function (error) {
                console.error('Error deleting image:', error);
            });
        };

        vm.removeProductVariation = function (variation) {
            if (confirm("Bạn có chắc chắn muốn xóa không")) {
                if (variation.ProductId != null) {
                    vm.deleteProductVariation(variation.ProductId, variation.ColorId, variation.SizeId);
                }

                vm.ProductVariation = vm.ProductVariation.filter(function (item) {
                    return item !== variation;
                });
            }
        };

        vm.deleteProductVariation = function (productId, colorId, sizeId) {
            serviceProxies.productVariationService.delete(productId, colorId, sizeId).then(function () {
                console.log('Deleted product variation');
            }).catch(function (error) {
                console.error('Error deleting product variation:', error);
            });
        };

        vm.isSaveDisabled = function () {
            // Kiểm tra các trường chính
            if (!vm.product.Name || !vm.product.Price || !vm.product.CategoryId) {
                return true;
            }
            // Kiểm tra các trường của từng variation
            for (let variation of vm.ProductVariation) {
                if (!variation.ColorId || !variation.SizeId || !variation.Quantity) {
                    return true;
                }
            }
            return false;
        };

    }

})(angular.module('app.admin.product'));
