(function (app) {
    app.controller('sizeController', sizeController)
        .component('size', {
            templateUrl: '/app/admin/size/size.component.html',
            controller: 'sizeController',
            controllerAs: 'vm'
        });

    sizeController.$inject = ['serviceProxies'];

    function sizeController(serviceProxies) {
        var vm = this;
        vm.sizes = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Name", field: "Name" },
            { headerName: "Description", field: "Description" }
        ];

        vm.getsizes = function () {
            serviceProxies.sizeService.getAll().then(function (response) {
                vm.sizes = response;
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.reloads = function () {
            vm.getsizes();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-size-modal')).controller('createOrEditSizeModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-size-modal')).controller('createOrEditSizeModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.colorService.delete(vm.selectedId).then(function () {
                    vm.getsizes();
                }).catch(function (error) {
                    console.error('Error deleting color:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getsizes();
    }

})(angular.module('app.admin.size'));