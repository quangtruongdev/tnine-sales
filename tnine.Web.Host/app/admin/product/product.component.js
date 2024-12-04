(function (app) {
    app.controller('productController', productController)
        .component('product', {
            templateUrl: '/app/admin/product/product.component.html',
            controller: 'productController',
            controllerAs: 'vm'
        });

    productController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function productController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.products = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1" },
                { headerName: "Id", field: "Id" },
                {
                    headerName: "Image",
                    field: 'ImgUrl',
                    cellRenderer: function (params) {
                        return `<img src="/wwwroot/${params.value}" style="width: 50px; height: 50px;" />`;
                    }
                },
                { headerName: "Name", field: "Name" },
                { headerName: "Price", field: "Price" },
                { headerName: "Category", field: "CategoryName" },
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
        };

        vm.getProducts = function () {
            serviceProxies.productService.getAll().then(function (response) {
                vm.products = response.Results;
                if (vm.gridApi) {
                    vm.gridApi.setRowData(vm.products);
                }
            }).catch(function (error) {
                console.error('Error fetching products:', error);
            });
        };

        vm.reloads = function () {
            vm.getProducts();
        };
        
        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-product-modal')).controller('createOrEditProductModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-product-modal')).controller('createOrEditProductModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.productService.delete(vm.selectedId).then(function () {
                    vm.getProducts();
                }).catch(function (error) {
                    console.error('Error deleting product:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getProducts();
    }

})(angular.module('app.admin.product'));