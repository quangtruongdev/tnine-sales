(function (app) {
    app.controller('dashboardController', dashboardController)
        .component('dashboard', {
            templateUrl: '/app/admin/dashboard/dashboard.component.html',
            controller: 'dashboardController',
            controllerAs: 'vm'
        });

    dashboardController.$inject = ['serviceProxies', '$element', '$timeout'];

    function dashboardController(serviceProxies, $element, $timeout) {
        var vm = this;
        vm.text = "Dashboard";
        vm.chartData = [];
        vm.chartLabels = [];
        vm.products = [];

        vm.getDashboardByMonth = function () {
            serviceProxies.dashboardService.getDashboardByMonth().then(function (response) {
                response.sort((a, b) => new Date(a.DateTime) - new Date(b.DateTime));

                vm.chartData = response.map(item => item.Value);
                vm.chartLabels = response.map(item => {
                    const date = new Date(item.DateTime);
                    const month = date.toLocaleString('default', { month: 'short' });
                    const year = date.getFullYear();
                    return `${month} / ${year}`; 
                });

                drawChart(vm.chartData, vm.chartLabels);
            }).catch(function (error) {
                console.error('Error fetching dashboard data:', error);
            });
        };


        vm.getProducts = function () {
            serviceProxies.dashboardService.getProductBestSaleOfMonth().then(function (response) {
                vm.products = response;
            }).catch(function (error) {

            });
        };

        vm.$onInit = function () {
            $timeout(function () {
                drawChart(vm.chartData, vm.chartLabels);
                vm.getDashboardByMonth();
                vm.getProducts();
            }, 0);
        };

        function drawChart(data, labels) {
            var canvas = document.getElementById('chartCanvas');
            if (!canvas) return;

            var ctx = canvas.getContext('2d');
            var width = 60;
            var spacing = 60;
            var maxHeight = Math.max(...data);
            var scaleFactor = (canvas.height - 40) / maxHeight;

            ctx.clearRect(0, 0, canvas.width, canvas.height); 

            data.forEach(function (value, index) {
                var height = value * scaleFactor;
                var x = index * (width + spacing) + spacing;
                var y = canvas.height - height -20;

                ctx.fillStyle = '#4CAF50';
                ctx.fillRect(x, y, width, height);

                ctx.fillStyle = '#000';
                ctx.textAlign = 'center';
                ctx.fillText(labels[index], x + width / 2, canvas.height - 5);
                ctx.font = '14px Arial';
                ctx.fillText(value.toLocaleString(), x + width / 2, y - 5);
            });
        }
    }
})(angular.module('app.admin.dashboard'));
