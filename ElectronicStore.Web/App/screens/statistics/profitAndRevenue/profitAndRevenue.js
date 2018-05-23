angular.module('electronicStoreApp.screens.statistics',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.services.orders',
        'electronicStoreApp.services.statistics',
        'angularUtils.directives.dirPagination',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common',
        'ngSanitize',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.bootstrap.modal'
	])
	.config(function ($stateProvider) {

	    $stateProvider
			.state('profitAndRevenue', {
			    url: "/statistics/profitAndRevenue",
			    parent: 'layout',
			    templateUrl: "/app/screens/statistics/profitAndRevenue/profitAndRevenue.html"
			});

	})
	.controller('profitAndRevenueController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'orderSvc', 'statisticSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, orderSvc, statisticSvc) {

            $scope.title = 'Thống kê doanh thu & lợi nhuận';
            $scope.selectedDate = new Date();

            Date.prototype.addDays = function (days) {
                var dat = new Date(this.valueOf());
                dat.setDate(dat.getDate() + days);
                return dat;
            }

            var revenueChart;
           
            var loadData = function () {
                var date = $filter('date')($scope.selectedDate, 'shortDate');
                statisticSvc.getProfitAndRevenue(date).then(function (response) {
                    $scope.sourceData = response.data;
                    //console.log($scope.sourceData);

                    var curr = new Date(date); // get current date
                    var first = curr.getDate() - curr.getDay(); // First day is the day of the month - the day of the week
                    var last = first + 6; // last day is the first day + 6


                    var firstday = new Date(curr.setDate(first));
                    var secondday = new Date(curr.setDate(first + 1));
                    var thirdday = new Date(curr.setDate(first + 2));
                    var fourthday = new Date(curr.setDate(first + 3));
                    var fifthday = new Date(curr.setDate(first + 4));
                    var sixthday = new Date(curr.setDate(first + 5));
                    var lastday = sixthday.addDays(1);

                    $scope.arrayDays = [];

                    $scope.arrayDays.push(firstday);
                    $scope.arrayDays.push(secondday);
                    $scope.arrayDays.push(thirdday);
                    $scope.arrayDays.push(fourthday);
                    $scope.arrayDays.push(fifthday);
                    $scope.arrayDays.push(sixthday);
                    $scope.arrayDays.push(lastday);
                    //console.log($scope.arrayDays);

                    var days = [];

                    angular.forEach($scope.arrayDays, function (d) {
                        days.push($filter('date')(d, 'MM/dd/yyyy'));
                    });

                    //console.log(days);

                    var chartData = {
                        labels: days,
                        datasets: [
                                {
                                    fillColor: "#00a65a",
                                    strokeColor: "#00a65a",
                                    pointColor: "#00a65a",
                                    pointStrokeColor: "#00a65a",
                                    pointHighlightFill: "#00a65a",
                                    pointHighlightStroke: "#00a65a",
                                    data: _.pluck($scope.sourceData, 'Revenue'),
                                    label: "Doanh thu"
                                },
                                {
                                    fillColor: "#f56954",
                                    strokeColor: "#f56954",
                                    pointColor: "#f56954",
                                    pointStrokeColor: "#f56954",
                                    pointHighlightFill: "#f56954",
                                    pointHighlightStroke: "#f56954",
                                    data: _.pluck($scope.sourceData, 'Profit'),
                                    label: "Lợi nhuận"
                                }
                        ]
                    };

                    var ctx = $("#revenueChart").get(0).getContext("2d");

                    if (revenueChart !== undefined) {
                        revenueChart.destroy();
                    }

                    revenueChart = new Chart(ctx).Bar(chartData, {
                        animation: true,
                        datasetFill: false,
                        bezierCurve: false,
                        scaleShowGridLines: false,
                        legendTemplate: "<ul class=\"dashboard-pie\"><% for (var i=0; i<datasets.length; i++){%><li><span class=\"dashboard-pie-legend dashboard-pie-<%=datasets[i].fillColor.slice(1)%>\" style=\"background-color:<%=datasets[i].fillColor%>\">&block;&nbsp;</span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
                    });

                    $scope.nrcLegend = revenueChart.generateLegend();

                }, function (error) {
                    console.log(error);
                });
            }

            loadData();

            $scope.refresh = function () {
                loadData();
            }
        }
	]);