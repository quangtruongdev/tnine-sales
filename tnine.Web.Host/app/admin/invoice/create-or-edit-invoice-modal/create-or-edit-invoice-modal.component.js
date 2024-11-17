
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

    CreateOrEditInvoiceModalController.$inject = ['serviceProxies'];

    function CreateOrEditInvoiceModalController(serviceProxies, $element) {
        var vm = this;

        vm.$onInit = function () {
            vm.invoice = {};
            vm.saving = false
            vm.listPaymentStatus = []
            vm.listPaymentMethods = []

            vm.isDisabled = false;
            vm.noCache = true;
            vm.searchTextPaymentStatus = '';
            vm.searchTextPaymentMethod = '';
            vm.selectedPaymentStatus = null;
            vm.selectedPaymentMethod = null;
        };

        function loadInitialData() {
            Promise.all([vm.getListPaymentStatus(), vm.getListPaymentMethods()])
                .catch(function (error) {
                    console.error('Error loading payment data:', error);
                });
        }

        vm.getListPaymentStatus = function () {
            return serviceProxies.paymentStatusService.getAll().then(function (response) {
                vm.listPaymentStatus = response;
            }).catch(function (error) {
                console.error('Error fetching payment status:', error);
            });
        }

        vm.getListPaymentMethods = function () {
            return serviceProxies.paymentMethodsService.getAll().then(function (response) {
                vm.listPaymentMethods = response;
            }).catch(function (error) {
                console.error('Error fetching payment status:', error);
            });
        }

        vm.show = function (id) {
            loadInitialData();

            if (!id) {
                vm.invoice = {};
                vm.selectedPaymentMethod = null;
                vm.selectedPaymentStatus = null;
                vm.searchTextPaymentMethod = '';
                vm.searchTextPaymentStatus = '';
            } else {
                serviceProxies.invoiceService.getInvoiceForEdit(id).then(function (response) {
                    vm.invoice = response.Invoice;

                    if (vm.invoice && vm.invoice.PaymentStatusId) {
                        vm.selectedPaymentStatus = vm.listPaymentStatus.find(function (status) {
                            return status.Id === vm.invoice.PaymentStatusId;
                        });
                        vm.searchTextPaymentStatus = vm.selectedPaymentStatus ? vm.selectedPaymentStatus.Name : '';
                    }

                    if (vm.invoice && vm.invoice.PaymentMethodId) {
                        vm.selectedPaymentMethod = vm.listPaymentMethods.find(function (method) {
                            return method.Id === vm.invoice.PaymentMethodId;
                        });
                        vm.searchTextPaymentMethod = vm.selectedPaymentMethod ? vm.selectedPaymentMethod.Name : '';
                    }
                }).catch(function (error) {
                    console.error('Error fetching invoice:', error);
                });
            }

            $('#createOrEditInvoiceModal').modal('show');
        };

        vm.save = function () {
            vm.saving = true;
            console.log(vm.invoice);

            serviceProxies.invoiceService.createOrEdit(vm.invoice).then(function () {
               vm.close();
               vm.onSaved();
            }).catch(function (error) {
               console.error('Error saving invoice:', error);
            }).finally(function () {
               vm.saving = false;
            });
        };

        vm.close = function () {
            $('#createOrEditInvoiceModal').modal('hide');
        };

        function createFilterFor(query) {
            var lowercaseQuery = query.toLowerCase();
            return function filterFn(item) {
                return item.Name.toLowerCase().indexOf(lowercaseQuery) === 0;
            };
        }

        vm.querySearchPaymentStatus = function (searchText) {
            return searchText ? vm.listPaymentStatus.filter(createFilterFor(searchText)) : vm.listPaymentStatus;
        };

        vm.querySearchPaymentMethod = function (searchText) {
            return searchText ? vm.listPaymentMethods.filter(createFilterFor(searchText)) : vm.listPaymentMethods;
        };

        vm.searchTextChangePaymentStatus = function (text) {
            console.log('Payment Status search text changed to ' + text);
        };

        vm.searchTextChangePaymentMethod = function (text) {
            console.log('Payment Method search text changed to ' + text);
        };

        vm.selectedItemChangePaymentStatus = function (item) {
            console.log('Payment Status selected item changed to ' + JSON.stringify(item));
            vm.invoice.PaymentStatusId = item ? item.Id : null;
        };

        vm.selectedItemChangePaymentMethod = function (item) {
            console.log('Payment Method selected item changed to ' + JSON.stringify(item));
            vm.invoice.PaymentMethodId = item ? item.Id : null;
        };
    }

})(angular.module('app.admin.invoice'));