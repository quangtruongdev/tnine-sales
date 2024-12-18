(function () {
    var dashboard = angular.module('tnine.dashboard', []);

    dashboard.controller('dashboardController', ['$scope', '$timeout', 'serviceProxies', function ($scope, $timeout, serviceProxies) {
        var vm = this;
        vm.totalRevenue = 0;
        vm.totalProducts = 0;
        vm.totalCustomers = 0;
        vm.averageInvoiceMonthly = 0;

        var revenueChart, categoryChart, topProductsChart, trafficChart;

        function init() {
            serviceProxies.dashboardService.getMasterData().then(function (result) {
                $scope.$applyAsync(function () {
                    vm.masterData = result;
                    vm.totalRevenue = vm.masterData.TotalRevenue;
                    vm.totalProducts = vm.masterData.TotalProduct;
                    vm.averageInvoiceMonthly = vm.masterData.TotalInvoice;
                    vm.totalCustomers = vm.masterData.TotalCustomer;
                    vm.productBestSales = vm.masterData.ProductBestSales;

                    vm.monthLabels = vm.masterData.RevenueMonthly.map(item => {
                        var date = new Date(item.DateTime);
                        return date.toLocaleString('default', { month: 'short' });
                    });
                    vm.revenueChartData = vm.masterData.RevenueMonthly.map(item => item.Value);
                    vm.additionalRevenueChartData = vm.masterData.RevenueMonthly.map(item => item.TotalInvoiceInMonth);

                    vm.categoryChartData = vm.masterData.CategoriesPercents.map(item => item.Value);
                    vm.categoryLabels = vm.masterData.CategoriesPercents.map(item => item.Name);

                    vm.topProductsData = vm.productBestSales.map(item => item.Quantity);
                    vm.productLabels = vm.productBestSales.map(item => item.ProductName);

                    vm.trafficLabels = vm.masterData.ProductSellIn12Months.map(item => {
                        var date = new Date(item.DateTime);
                        return date.toLocaleString('default', { month: 'short' });
                    });
                    vm.trafficData = vm.masterData.ProductSellIn12Months.map(item => item.Quantity);

                    drawRevenueChart();
                    drawCategoryChart();
                    drawTopProductsChart();
                    drawTrafficChart();
                });
            }).catch(function (error) {
                console.error('Error fetching master data:', error);
            });
        }

        vm.$onInit = function () {
            $timeout(function () {
                init();
            }, 0);
        };

        function drawRevenueChart() {
            var ctx = document.getElementById('revenueChart').getContext('2d');
            if (revenueChart) {
                revenueChart.destroy();
            }
            revenueChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: vm.monthLabels,
                    datasets: [{
                        label: 'Monthly Revenue (VND)',
                        data: vm.revenueChartData,
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 2
                    }],
                }
            });
        }

        function drawCategoryChart() {
            var ctx = document.getElementById('categoryChart').getContext('2d');
            if (categoryChart) {
                categoryChart.destroy();
            }
            categoryChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: vm.categoryLabels,
                    datasets: [{
                        data: vm.categoryChartData,
                        backgroundColor: [
                            '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40', '#FFCD56', '#C9CBCF'
                        ]
                    }]
                }
            });
        }

        function drawTopProductsChart() {
            var ctx = document.getElementById('topProductsChart').getContext('2d');
            if (topProductsChart) {
                topProductsChart.destroy();
            }
            topProductsChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: vm.productLabels,
                    datasets: [{
                        label: 'Quantity Sold',
                        data: vm.topProductsData,
                        backgroundColor: 'rgba(75, 192, 192, 0.6)'
                    }]
                }
            });
        }

        function drawTrafficChart() {
            var ctx = document.getElementById('trafficChart').getContext('2d');
            if (trafficChart) {
                trafficChart.destroy();
            }
            trafficChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: vm.trafficLabels,
                    datasets: [{
                        label: 'Product Quantity',
                        data: vm.trafficData,
                        backgroundColor: 'rgba(153, 102, 255, 0.2)',
                        borderColor: 'rgba(153, 102, 255, 1)',
                        borderWidth: 2
                    }]
                }
            });
        }
    }]);

    dashboard.component('dashboard', {
        templateUrl: '/app/admin/dashboard/dashboard.component.html',
        controller: 'dashboardController',
        controllerAs: 'vm'
    });
})();