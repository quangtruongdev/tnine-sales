(function (app) {
    app.directive('tnineMultiselect', tnineMultiselect);

    function tnineMultiselect() {
        return {
            restrict: 'E',
            templateUrl: '/app/shared/common/input-types/tnine-multiselect/tnine-multiselect.component.html',
            controller: tnineMultiselectController,
            controllerAs: 'vm',
            scope: {
                options: '=',
                text: '@',
                isRequired: '<',
                isDisabled: '<',
            },
            bindToController: true
        };
    }

    tnineMultiselectController.$inject = ['$scope'];

    function tnineMultiselectController($scope) {
        var vm = this;

        vm.options = $scope.options;
        vm.text = $scope.text;
        vm.isRequired = $scope.isRequired || false;
        vm.isDisabled = $scope.isDisabled || false;
        vm.selectedItems = [];

        vm.toggleSelection = function (option) {
            if (vm.isDisabled) return;

            const index = vm.selectedItems.indexOf(option);
            if (index > -1) {
                vm.selectedItems.splice(index, 1);
            } else {
                vm.selectedItems.push(option);
            }
        };

        vm.isSelected = function (option) {
            return vm.selectedItems.includes(option);
        };
        
    }
})(angular.module('app.common'));