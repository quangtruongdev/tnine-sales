
(function (app) {
    app.controller('ViewDetailModalController', ViewDetailModalController)
        .component('viewDetailModal', {
            templateUrl: '/app/admin/invoice/view-detail-modal/view-detail-modal.component.html',
            controller: 'ViewDetailModalController',
            controllerAs: 'vm',
            bindings: {
                onSaved: '&',
            },
        });

    ViewDetailModalController.$inject = ['serviceProxies', '$timeout', '$scope'];

    function ViewDetailModalController(serviceProxies, $timeout, $scope) {
        var vm = this;

        vm.show = (id) => {
            vm.invoiceDetail = {}

            serviceProxies.invoiceService.getInvoiceDetails(id)
                .then(result => vm.invoiceDetail = result)
                .catch((error) => console.error('Error fetching invoice details:', error))

            $('#viewDetailModal').modal('show');
        };

        vm.close = () => $('#viewDetailModal').modal('hide');
    }
})(angular.module('app.admin.invoice'));