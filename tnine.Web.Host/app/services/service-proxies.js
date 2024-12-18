(function (app) {
    app.factory('serviceProxies', serviceProxies);

    serviceProxies.$inject = ['$http', '$q', '$injector', 'baseService'];

    function serviceProxies($http, $q, $injector, baseService) {
        var serviceProxies = {};

        serviceProxies.accountService = {
            login: function (data) {
                return baseService.post('api/account/login', data);
            },
            logout: function () {
                return baseService.post('api/account/logout');
            },
            register: function (data) {
                return baseService.post('api/account/register', data);
            },
            getAccountInfo: function () {
                return baseService.get('api/account/getAccountInfo');
            }
        };

        serviceProxies.userService = {
            getAll: function () {
                return baseService.get('api/user');
            },
            getById: function (id) {
                return baseService.get('api/user/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/user', data);
            },
            delete: function (id) {
                return baseService.remove('api/user/' + id);
            }
        };

        serviceProxies.roleService = {
            getAll: function () {
                return baseService.get('api/role');
            },
            getById: function (id) {
                return baseService.get('api/role/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/role', data);
            },
            delete: function (id) {
                return baseService.remove('api/role/' + id);
            },
            hasRole: function (roleName) {
                var userInfo = JSON.parse(sessionStorage.getItem('userInfo'));
                return userInfo && userInfo.role && userInfo.role.includes(roleName);
            }
        };

        serviceProxies.userService = {
            register: function (data) {
                return baseService.post('api/account/register', data);
            },
            getAll: function () {
                return baseService.get('api/user/');
            },
            getById: function (id) {
                return baseService.get('api/user/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/user', data);
            },
            delete: function (id) {
                return baseService.remove('api/user/' + id);
            }
        };

        serviceProxies.customerService = {
            getAll: function () {
                return baseService.get('api/customer');
            },
            getCustomerForEdit: function (id) {
                return baseService.get('api/customer/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/customer', data);
            },
            delete: function (id) {
                return baseService.remove('api/customer/' + id);
            }
        };

        serviceProxies.productService = {
            getAll: function () {
                return baseService.get('api/product');
            },
            getProductForEdit: function (id) {
                return baseService.get('api/product/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/product', data);
            },
            delete: function (id) {
                return baseService.remove('api/product/' + id);
            },
            getListColors: function () {
                return baseService.get('api/color');
            },
            getListSizes: function () {
                return baseService.get('api/size');
            },
            getListImages: function (id) {
                return baseService.get('api/image/' + id);
            }
        };

        serviceProxies.paymentStatusService = {
            getAll: function () {
                return baseService.get('api/PaymentStatus');
            },
            getById: function (id) {
                return baseService.get('api/PaymentStatus/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/PaymentStatus', data);
            },
            delete: function (id) {
                return baseService.remove('api/PaymentStatus/' + id);
            }
        };

        serviceProxies.categoryService = {
            getAll: function () {
                return baseService.get('api/category');
            },
            getCategoryForEdit: function (id) {
                return baseService.get('api/category/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/category', data);
            },
            delete: function (id) {
                return baseService.remove('api/category/' + id);
            }
        };

        serviceProxies.invoiceService = {
            getAll: function () {
                return baseService.get('api/invoice');
            },
            getInvoiceForEdit: function (id) {
                return baseService.get('api/invoice/' + id);
            },
            createOrEdit: function (invoice, items) {
                return baseService.post('api/invoice', { invoice: invoice, items: items });
            },
            delete: function (id) {
                return baseService.remove('api/invoice/' + id);
            },
            getInvoiceDetails: function (id) {
                return baseService.get('api/invoice/details/' + id);
            },
        };

        serviceProxies.colorService = {
            getAll: function () {
                return baseService.get('api/color');
            },
            getById: function (id) {
                return baseService.get('api/color/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/color/', data);
            },
            delete: function (id) {
                return baseService.remove('api/color/' + id);
            }
        };

        serviceProxies.sizeService = {
            getAll: function () {
                return baseService.get('api/size');
            },
            getById: function (id) {
                return baseService.get('api/size/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/size/', data);
            },
            delete: function (id) {
                return baseService.remove('api/size/' + id);
            }
        };

        serviceProxies.paymentMethodsService = {
            getAll: function () {
                return baseService.get('api/PaymentMethods');
            },
            getById: function (id) {
                return baseService.get('api/PaymentMethods/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/PaymentMethods', data);
            },
            delete: function (id) {
                return baseService.remove('api/PaymentMethods/' + id);
            }
        };

        serviceProxies.productVariationService = {
            getAll: function () {
                return baseService.get('api/productVariation');
            },
            getForEdit: function (id) {
                return baseService.get('api/productVariation/' + id);
            },
            delete: function (productId, colorId, sizeId) {
                return baseService.remove('api/productVariation/' + productId + '/' + colorId + '/' + sizeId);
            },
            createOrEdit: function (data) {
                return baseService.post('api/productVariation/', data);
            }
        };

        serviceProxies.imageService = {
            upLoad: function (data) {
                return baseService.post('api/image/upload', data);
            },
            createOrEdit: function (data) {
                return baseService.post('api/image', data);
            },
            delete: function (id) {
                return baseService.remove('api/image/' + id);
            }
        };

        serviceProxies.supplierService = {
            getAll: function () {
                return baseService.get('api/supplier');
            },
            getById: function (id) {
                return baseService.get('api/supplier/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/supplier/', data);
            },
            delete: function (id) {
                return baseService.remove('api/supplier/' + id);
            }
        };

        serviceProxies.dashboardService = {
            getDashboardByMonth: function () {
                return baseService.get('api/dashboard');
            },
            getProductBestSaleOfMonth: function () {
                return baseService.get('api/dashboard/product-best-sales');
            },
            getMasterData: function () {
                return baseService.get('api/dashboard/master-data');
            }
        };

        serviceProxies.warehouseReceiptService = {
            getAll: function () {
                return baseService.get('api/warehouse-receipt');
            },
            getWarehouseReceiptForEdit: function (id) {
                return baseService.get('api/warehouse-receipt/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/warehouse-receipt', data);
            },
            delete: function (id) {
                return baseService.remove('api/warehouse-receipt/' + id);
            },
            getTotal: function (id) {
                return baseService.get('api/warehouse-receipt/total/' + id);
            },
            getSupplier: function (id) {
                return baseService.get('api/warehouse-receipt/supplier/' + id)
            },
            getListSuppliers: function () {
                return baseService.get('api/supplier');
            },
            getListColors: function () {
                return baseService.get('api/color');
            },
            getListSizes: function () {
                return baseService.get('api/size');
            },
            getListProducts: function (id) {
                return baseService.get('api/product');
            }
        };

        serviceProxies.orderService = {
            getAll: function () {
                return baseService.get('api/order');
            },
            getOrderForEdit: function (id) {
                return baseService.get('api/order/' + id);
            },
            createOrEdit: function (data) {
                return baseService.post('api/order', data);
            },
            delete: function (id) {
                return baseService.remove('api/order/' + id);
            }
        };

        return serviceProxies;
    }

})(angular.module('app.services'));