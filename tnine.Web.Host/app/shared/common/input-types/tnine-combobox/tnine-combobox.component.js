(function (app) {
    app.directive('tnineCombobox', tnineCombobox);

    function tnineCombobox() {
        return {
            restrict: 'E',
            templateUrl: '/app/shared/common/input-types/tnine-combobox/tnine-combobox.component.html',
            controller: tnineComboboxController,
            controllerAs: 'vm',
            scope: {
                hasCheck: '<',
                class: '<',
                value: '=',
                items: '=',
                text: '@',
                isRequired: '<',
                isValidate: '<',
                isDisabled: '<',
                label: '@',
                inputLabel: '@',
                key: '@',
                placeholder: '@',
                hasFilter: '<',
            },
            bindToController: true
        };
    }

    tnineComboboxController.$inject = ['$scope'];

    function tnineComboboxController($scope) {
        var vm = this;

        vm.hasCheck = $scope.hasCheck || false;
        vm.class = $scope.class || '';
        vm.selectedItem = $scope.selectedItem;
        vm.items = $scope.items;
        vm.text = $scope.text;
        vm.isRequired = $scope.isRequired || false;
        vm.isValidate = $scope.isValidate || false;
        vm.isDisabled = $scope.isDisabled || false;
        vm.selectedItem = null;
        vm.label = $scope.label || 'label';
        vm.inputLabel = $scope.inputLabel || '';
        vm.key = $scope.key || 'value';
        vm.placeholder = $scope.placeholder || '';
        vm.hasFilter = $scope.hasFilter || true;

        vm.errorMessage = 'This field is required';

        $scope.$watch('vm.items', function (newVal, oldVal) {
            if (newVal) {
                vm.items = newVal;
            }
        });

        vm.onSelectChange = function () {
            vm.value = vm.selectedItem;

            if (vm.isRequired && !vm.value) {
                vm.isValidate = true;
            } else {
                vm.isValidate = false;
            }
        };

        vm.$onInit = function () {
            if (vm.isRequired && !vm.value) {
                vm.errorMessage = vm.label + ' is required';
            }
        };

        $scope.$watch('vm.value', function (newVal, oldVal) {
            if (newVal) {
                vm.selectedItem = newVal;
                vm.isValidate = false;
            }
        });
    }

})(angular.module('app.common'));
