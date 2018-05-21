angular.module('electronicStoreApp.screens.statistics.revenue',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.services.orders',
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
			.state('revenue', {
			    url: "/statistics/revenue",
			    parent: 'layout',
			    templateUrl: "/app/screens/statistics/revenue/revenue.html"
			});

	})
	.controller('revenueController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'orderSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, orderSvc) {

            $scope.title = 'Thống kê doanh thu';
            $scope.selectedDate = new Date();
            $scope.refresh = function () {
                console.log($scope.selectedDate);
            }
        }
	]);