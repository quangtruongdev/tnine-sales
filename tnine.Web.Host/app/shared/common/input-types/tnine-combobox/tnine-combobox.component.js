(function (app) {
    app.component('tnineCombobox', {
        templateUrl: '/app/shared/common/input-types/tnine-combobox/tnine-combobox.component.html',
        controller: 'tnineComboboxController',
        controllerAs: 'vm',
        bindings: {
            model: '=',
            items: '<',
            displayMember: '@',
            valueMember: '@',
            placeholder: '@',
            required: '<',
            disabled: '<',
            onChange: '&'
        }
    });

    app.controller('tnineComboboxController', tnineComboboxController);

    tnineComboboxController.$inject = ['$scope'];

    function tnineComboboxController($scope) {
        var vm = this;

        vm.$onInit = function () {
            vm.displayMember = vm.displayMember || 'name';
            vm.valueMember = vm.valueMember || 'id';
            vm.placeholder = vm.placeholder || '';
            vm.required = vm.required || false;
            vm.disabled = vm.disabled || false;

            // Khởi tạo biến tìm kiếm
            vm.searchText = '';
            vm.filteredItems = vm.items || [];
        };

        vm.filterItems = function () {
            var search = vm.searchText.toLowerCase();
            vm.filteredItems = vm.items.filter(function (item) {
                return item[vm.displayMember].toLowerCase().indexOf(search) > -1;
            });
        };

        vm.selectItem = function (item) {
            vm.model = item[vm.valueMember];
            vm.searchText = item[vm.displayMember];
            vm.filteredItems = [];
            vm.onChangeInternal();
        };

        vm.onChangeInternal = function () {
            if (vm.onChange) {
                vm.onChange();
            }
        };
    }
})(angular);
