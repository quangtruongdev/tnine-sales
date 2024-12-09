﻿(function (app) {
    app.component('homeTest', {
        templateUrl: '/app/pages/home/hometest.html',
        controller: homeTestController,
        controllerAs: 'vm',
    });

    homeTestController.$inject = ['serviceProxies'];

    function homeTestController(serviceProxies) {
        var vm = this;

        vm.dailyRevenue = 0;
        vm.monthlyRevenue = 0;
        vm.yearlyRevenue = 0;

        vm.loadRevenueData = function () {
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth() + 1;
            var day = today.getDate();

            serviceProxies.reportService.getDailyRevenue(date).then(function (response) {
                vm.dailyRevenue = response.data;
                vm.renderChart();
            });

            serviceProxies.reportService.getMonthlyRevenue(date).then(function (response) {
                vm.monthlyRevenue = response.data;
                vm.renderChart();
            });

            serviceProxies.reportService.getYearlyRevenue(date).then(function (response) {
                vm.yearlyRevenue = response.data;
                vm.renderChart();
            });
        };

        vm.renderChart = function () {
            var ctx = document.getElementById('revenueChart').getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['Daily', 'Monthly', 'Yearly'],
                    datasets: [{
                        label: 'Revenue',
                        data: [vm.dailyRevenue, vm.monthlyRevenue, vm.yearlyRevenue],
                        backgroundColor: ['rgba(75, 192, 192, 0.2)'],
                        borderColor: ['rgba(75, 192, 192, 1)'],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        };

        vm.$onInit = function () {
            vm.loadRevenueData();
        };
    }

})(angular.module('tnine.home'));