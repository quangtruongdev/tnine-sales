
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

    function CreateOrEditInvoiceModalController(serviceProxies) {
        var vm = this;

        vm.$onInit = function () {
            vm.invoice = {}
            vm.saving = false
            vm.listPaymentStatus = []
            vm.listPaymentMethods = []
            vm.searchTextPaymentStatus = ''
            vm.searchTextPaymentMethod = ''
            vm.selectedPaymentStatus = null
            vm.selectedPaymentMethod = null
        };

        function loadInitialData() {
            Promise.all([vm.getListPaymentStatus(), vm.getListPaymentMethods()])
                .catch((error) => console.error('Error loading payment data:', error))
        }

        vm.getListPaymentStatus = () => 
            serviceProxies.paymentStatusService.getAll()
                .then((response) => vm.listPaymentStatus = response)
                .catch((error) =>console.error('Error fetching payment status:', error))

        vm.getListPaymentMethods = () => 
            serviceProxies.paymentMethodsService.getAll()
                .then(response => vm.listPaymentMethods = response)
                .catch(error => console.error('Error fetching payment method:', error))

        vm.show = function (id) {
            loadInitialData();

            if (!id) {
                vm.invoice = {}
                vm.selectedPaymentMethod =  vm.selectedPaymentStatus = null
                vm.searchTextPaymentMethod = vm.searchTextPaymentStatus = ''
            } else {
                serviceProxies.invoiceService.getInvoiceForEdit(id).then(response => {
                    vm.invoice = response.Invoice;
                    setSelectedItem(vm.invoice.PaymentStatusId, vm.listPaymentStatus, 'selectedPaymentStatus', 'searchTextPaymentStatus');
                    setSelectedItem(vm.invoice.PaymentMethodId, vm.listPaymentMethods, 'selectedPaymentMethod', 'searchTextPaymentMethod');
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

            serviceProxies.invoiceService.createOrEdit(vm.invoice)
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

        const querySearch = (list, searchText) => searchText ? list.filter(createFilterFor(searchText)) : list;

        const createFilterFor = searchText => item => item.Name.toLowerCase().startsWith(searchText.toLowerCase());

        vm.selectedItemChangePaymentStatus = item => vm.invoice.PaymentStatusId = item ? item.Id : null;
        vm.selectedItemChangePaymentMethod = item => vm.invoice.PaymentMethodId = item ? item.Id : null;
    }
})(angular.module('app.admin.invoice'));