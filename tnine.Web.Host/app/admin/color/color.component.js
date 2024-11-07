(function (app) {
    app.controller('colorController', colorController)
        .component('color', {
            templateUrl: '/app/admin/color/color.component.html',
            controller: 'colorController',
            controllerAs: 'vm'
        });

    colorController.$inject = ['serviceProxies'];

    function colorController(serviceProxies) {
        var vm = this;
        vm.colors = [];
        vm.selectedId = null;

        vm.columns = [
            { headerName: "Id", field: "Id" },
            { headerName: "Code", field: "Code" },
            { headerName: "HexCode", field: "HexCode" }
        ];

        vm.getcolors = function () {
            serviceProxies.colorService.getAll().then(function (response) {
                vm.colors = response;
            }).catch(function (error) {
                console.error('Error fetching colors:', error);
            });
        };

        vm.reloads = function () {
            vm.getcolors();
        };

        vm.add = function () {
            angular.element(document.querySelector('create-or-edit-color-modal')).controller('createOrEditColorModal').show();
        };

        vm.edit = function () {
            angular.element(document.querySelector('create-or-edit-color-modal')).controller('createOrEditColorModal').show(vm.selectedId);
        };

        vm.delete = function () {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.colorService.delete(vm.selectedId).then(function () {
                    vm.getcolors();
                }).catch(function (error) {
                    console.error('Error deleting color:', error);
                });
            }
        };

        vm.handleRowClick = function (row) {
            vm.selectedId = row.Id;
        };

        vm.getcolors();
    }

})(angular.module('app.admin.color'));