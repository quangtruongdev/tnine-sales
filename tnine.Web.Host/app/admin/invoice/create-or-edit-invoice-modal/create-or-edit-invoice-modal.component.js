
(function (app) {
    app.controller('CreateOrEditInvoiceModalController', CreateOrEditInvoiceModalController)
        .component('createOrEditInvoiceModal', {
            templateUrl: '/app/admin/invoice/create-or-edit-invoice-modal/create-or-edit-invoice-modal.component.html',
            controller: 'CreateOrEditInvoiceModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    CreateOrEditInvoiceModalController.$inject = ['serviceProxies', '$timeout', '$scope'];

    function CreateOrEditInvoiceModalController(serviceProxies, $timeout, $scope) {
        var vm = this;

        vm.$onInit = function () {
            vm.invoice = {}
            vm.saving = false
            vm.listPaymentStatus = []
            vm.listPaymentMethods = []
            vm.listCustomers = []
            vm.searchTextPaymentStatus = ''
            vm.searchTextPaymentMethod = ''
            vm.searchTextCustomer = ''
            vm.selectedPaymentStatus = null
            vm.selectedPaymentMethod = null
            vm.selectedCustomer = null

            vm.cart = []
            vm.listProducts = []
            vm.listSizes = []
            vm.listColors = []
            vm.listProductVariations = []
            vm.selectedProductId = null

            $scope.$watch(
                () => vm.cart, 
                () => vm.caculateTotal(),
                true
            );
            $scope.$watch(
                () => vm.cart.map(item => item.sizeId),
                (newValues, oldValues) => {
                    newValues.forEach((newValue, index) => {
                        if (newValue !== oldValues[index]) {
                            vm.updateItemLeft(vm.cart[index]);
                        }
                    });
                },
                true
            );

            $scope.$watch(
                () => vm.cart.map(item => item.colorId),
                (newValues, oldValues) => {
                    newValues.forEach((newValue, index) => {
                        if (newValue !== oldValues[index]) {
                            vm.updateItemLeft(vm.cart[index]);
                        }
                    });
                },
                true
            );
        };

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1", maxWidth: 50 },
                { headerName: "Name", field: "Name" },
                { headerName: "Price", field: "Price", maxWidth: 150 },
                { headerName: "Category", field: "CategoryName" },
            ],
            rowData: [],
            pagination: true,
            paginationPageSize: 10,
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
                $timeout(() => vm.selectedProductId = event.data.Id)
            }
        };

        function loadInitialData() {
            Promise.all([
                vm.getListPaymentStatus(),
                vm.getListPaymentMethods(),
                vm.getListProducts(),
                vm.getListSizes(),
                vm.getListColors(),
                vm.getListCustomers(),
                vm.getListProductVariations()
            ])
                .catch((error) => console.error('Error loading payment data:', error))
        }

        vm.getListProducts = () =>
            serviceProxies.productService.getAll()
                .then((response) => {
                    vm.listProducts = response.Results

                    if (vm.gridApi) {
                        vm.gridApi.setRowData(vm.listProducts);
                    }
                })
                .catch((error) => console.error('Error fetching products:', error))

        vm.getListSizes = () =>
            serviceProxies.sizeService.getAll()
                .then((response) => vm.listSizes = response)
                .catch((error) => console.error('Error fetching sizes:', error))

        vm.getListColors = () =>
            serviceProxies.colorService.getAll()
                .then((response) => vm.listColors = response)
                .catch((error) => console.error('Error fetching colors:', error))

        vm.getListProductVariations = () =>
            serviceProxies.productVariationService.getAll()
                .then((response) => vm.listProductVariations = response)
                .catch((error) => console.error('Error fetching product variations:', error))

        vm.getListCustomers = () =>
            serviceProxies.customerService.getAll()
                .then((response) => vm.listCustomers = response)
                .catch((error) => console.error('Error fetching customers:', error))

        vm.addProductToCart = () => {
            const isProductInCart = vm.cart.some(p => p.Id === vm.selectedProductId)
            if (isProductInCart) return

            const product = vm.listProducts.find(p => p.Id === vm.selectedProductId)
            const listSizes = vm.listSizes.filter(s => vm.listProductVariations.some(pv => pv.ProductId === product.Id && pv.SizeId === s.Id))
            const listColors = vm.listColors.filter(c => vm.listProductVariations.some(pv => pv.ProductId === product.Id && pv.ColorId === c.Id))
            vm.cart.push({ ...product, quantity: 1, itemLeft: 0, sizeId: null, colorId: null, listSizes, listColors });
        
            vm.caculateTotal();
            console.log(vm.cart);
            console.log(vm.invoice);
            
        }

        vm.updateItemLeft = function(item) {
            const productVariation = vm.listProductVariations.find(variation => 
                variation.SizeId === item.sizeId && variation.ColorId === item.colorId && variation.ProductId === item.Id);
            item.itemLeft = productVariation ? productVariation.Quantity : 0;
            if (item.quantity > item.itemLeft) {
                item.quantity = item.itemLeft;
            }
        };
        
        vm.removeProductFromCart = function(index) {
            vm.cart.splice(index, 1);
            vm.calculateTotal();
        };

        vm.calculateTotal = () => {
            vm.cart.forEach(item => {
                if (item.quantity > item.itemLeft) {
                    item.quantity = item.itemLeft;
                }
            });
            vm.invoice.Total = vm.cart.reduce((total, product) => total + product.Price * product.quantity, 0);
        };

        vm.getListPaymentStatus = () =>
            serviceProxies.paymentStatusService.getAll()
                .then((response) => vm.listPaymentStatus = response)
                .catch((error) => console.error('Error fetching payment status:', error))

        vm.getListPaymentMethods = () =>
            serviceProxies.paymentMethodsService.getAll()
                .then(response => vm.listPaymentMethods = response)
                .catch(error => console.error('Error fetching payment method:', error))

        vm.show = function (id) {
            loadInitialData();

            if (!id) {
                vm.invoice = {}
                vm.selectedPaymentMethod = vm.selectedPaymentStatus = vm.selectedCustomer = null
                vm.searchTextPaymentMethod = vm.searchTextPaymentStatus = vm.searchTextCustomer = ''
                vm.cart = []
                vm.selectedProductId = null
            } else {
                serviceProxies.invoiceService.getInvoiceForEdit(id).then(response => {
                    vm.invoice = response.Invoice;
                    setSelectedItem(vm.invoice.PaymentStatusId, vm.listPaymentStatus, 'selectedPaymentStatus', 'searchTextPaymentStatus');
                    setSelectedItem(vm.invoice.PaymentMethodId, vm.listPaymentMethods, 'selectedPaymentMethod', 'searchTextPaymentMethod');
                    
                    if (vm.invoice.CustomerId) {
                        const customer = vm.listCustomers.find(c => c.Id === vm.invoice.CustomerId);
                        vm.selectedCustomer = customer;
                        vm.searchTextCustomer = customer ? customer.FullName : '';
                    }
                }).catch(error => console.error('Error fetching invoice:', error));
            }

            $('#createOrEditInvoiceModal').modal('show');
        };

        const setSelectedItem = (itemId, list, selectedItemProp, searchTextProp) => {
            const item = list.find(item => item.Id === itemId);
            vm[selectedItemProp] = item;
            vm[searchTextProp] = item ? item.Name : '';
        }

        vm.save = () => {
            vm.saving = true;
            vm.invoice.creationTime = new Date();
            let items = vm.cart.map(product => {
                return {
                    ProductId: product.Id,
                    Quantity: product.quantity,
                    SizeId: product.sizeId,
                    ColorId: product.colorId
                }
            });

            serviceProxies.invoiceService.createOrEdit(vm.invoice, items)
                .then(() => {
                    vm.close();
                    vm.onSaved();
                })
                .catch(error => console.error('Error saving invoice:', error))
                .finally(() => vm.saving = false);
        };

        vm.close = () => $('#createOrEditInvoiceModal').modal('hide');

        vm.querySearchPaymentStatus = searchText => querySearch(vm.listPaymentStatus, searchText);
        vm.querySearchPaymentMethod = searchText => querySearch(vm.listPaymentMethods, searchText);
        vm.querySearchCustomer = (searchText) => {
            return !!searchText ? vm.listCustomers.filter((customer) => {
                return customer.FullName.toLowerCase().startsWith(searchText.toLowerCase());
            }) : vm.listCustomers;
        };

        const querySearch = (list, searchText) => searchText ? list.filter(createFilterFor(searchText)) : list;

        const createFilterFor = searchText => item => item.Name.toLowerCase().startsWith(searchText.toLowerCase());

        vm.selectedItemChangePaymentStatus = item => vm.invoice.PaymentStatusId = item ? item.Id : null;
        vm.selectedItemChangePaymentMethod = item => vm.invoice.PaymentMethodId = item ? item.Id : null;
        vm.selectedItemChangeCustomer = item => vm.invoice.CustomerId = item ? item.Id : null;
    }
})(angular.module('app.admin.invoice'));