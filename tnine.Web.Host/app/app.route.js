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

    ]);
})(angular.module('root'));