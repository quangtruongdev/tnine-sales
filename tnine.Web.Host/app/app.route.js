(function (app) {
    app.constant('ROUTES', [
        {
            name: 'home',
            url: '/home',
            templateUrl: '/app/pages/home/home.component.html',
            controller: 'homeController',
            files: [
                '/app/pages/home/home.module.js',
                '/app/pages/home/home.component.js',
                '/app/pages/home/hometest.js'
            ],
            requiresAuth: true,
        },
        {
            name: 'todo',
            url: '/todo',
            templateUrl: '/app/pages/todo/todo.component.html',
            controller: 'todoController',
            files: [
                '/app/pages/todo/todo.module.js',
                '/app/pages/todo/todo.component.js',
                '/app/pages/todo/create-or-edit-todo-modal/create-or-edit-todo-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'role',
            url: '/role',
            templateUrl: '/app/admin/authorization/role/role.component.html',
            controller: 'roleController',
            files: [
                '/app/admin/authorization/role/role.module.js',
                '/app/admin/authorization/role/role.component.js',
                '/app/admin/authorization/role/create-or-edit-role-modal/create-or-edit-role-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'user',
            url: '/user',
            templateUrl: '/app/admin/authorization/user/user.component.html',
            controller: 'userController',
            files: [
                '/app/admin/authorization/user/user.module.js',
                '/app/admin/authorization/user/user.component.js',
                '/app/admin/authorization/user/create-or-edit-user-modal/create-or-edit-user-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'permission',
            url: '/permission',
            templateUrl: '/app/admin/authorization/permission/permission.component.html',
            controller: 'permissionController',
            files: [
                '/app/admin/authorization/permission/permission.module.js',
                '/app/admin/authorization/permission/permission.component.js',
                '/app/admin/authorization/permission/create-or-edit-permission-modal/create-or-edit-permission-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'login',
            url: '/login',
            templateUrl: '/app/account/login/login.component.html',
            controller: 'loginController',
            files: [
                '/app/account/account.module.js',
                '/app/account/login/login.component.js'
            ],
            requiresAuth: false,
            roles: []
        },
        {
            name: 'customer',
            url: '/customer',
            templateUrl: '/app/admin/customer/customer.component.html',
            controller: 'customerController',
            files: [
                '/app/admin/customer/customer.module.js',
                '/app/admin/customer/customer.component.js',
                '/app/admin/customer/create-or-edit-customer-modal/create-or-edit-customer-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'product',
            url: '/product',
            templateUrl: '/app/pages/product/product.component.html',
            controller: 'productController',
            files: [
                '/app/pages/product/product.module.js',
                '/app/pages/product/product.component.js',
                '/app/pages/product/create-or-edit-product-modal/create-or-edit-product-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'paymentStatus',
            url: '/paymentStatus',
            templateUrl: '/app/pages/paymentStatus/payment-status.component.html',
            controller: 'paymentStatusController',
            files: [
                '/app/pages/paymentStatus/payment-status.module.js',
                '/app/pages/paymentStatus/payment-status.component.js',
                '/app/pages/paymentStatus/create-or-edit-payment-status-modal/create-or-edit-payment-status-modal.component.js'
            ]
        },
        {
            name: 'color',
            url: '/color',
            templateUrl: '/app/admin/color/color.component.html',
            controller: 'colorController',
            files: [
                '/app/admin/color/color.module.js',
                '/app/admin/color/color.component.js',
                '/app/admin/color/create-or-edit-color-modal/create-or-edit-color-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {

            name: 'invoice',
            url: '/invoice',
            templateUrl: '/app/admin/invoice/invoice.component.html',
            controller: 'invoiceController',
            files: [
                '/app/admin/invoice/invoice.module.js',
                '/app/admin/invoice/invoice.component.js',
                '/app/admin/invoice/create-or-edit-invoice-modal/create-or-edit-invoice-modal.component.js'
            ]
        },
        {
            name: 'size',
            url: '/size',
            templateUrl: '/app/admin/size/size.component.html',
            controller: 'sizeController',
            files: [
                '/app/admin/size/size.module.js',
                '/app/admin/size/size.component.js',
                '/app/admin/size/create-or-edit-size-modal/create-or-edit-size-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {

            name: 'shop',
            url: '/shop',
            templateUrl: '/app/pages/shop/shop.component.html',
            controller: 'shopController',
            files: [
                '/app/pages/shop/shop.module.js',
                '/app/pages/shop/shop.component.js',
                '/app/pages/shop/create-or-edit-shop-modal/create-or-edit-shop-modal.component.js'
            ],
            requiresAuth: true,
            roles: ['admin']
        },
        {
            name: 'paymentMethods',
            url: '/paymentMethods',
            templateUrl: '/app/pages/paymentMethods/payment-methods.component.html',
            controller: 'paymentMethodsController',
            files: [
                '/app/pages/paymentMethods/payment-methods.module.js',
                '/app/pages/paymentMethods/payment-methods.component.js',
                '/app/pages/paymentMethods/create-or-edit-payment-methods-modal/create-or-edit-payment-methods-modal.component.js'
            ]
        },
    ]);
})(angular.module('root'));