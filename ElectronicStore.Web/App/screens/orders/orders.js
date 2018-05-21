angular.module('electronicStoreApp.screens.orders',
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
			.state('orders', {
			    url: "/orders",
			    parent: 'layout',
			    templateUrl: "/app/screens/orders/list.html"
			});

	})
	.controller('orderListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'orderSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, orderSvc) {

            $scope.title = 'Danh sách đơn hàng';
            $scope.loading = true;
            $scope.keyword = '';
  

            var getListOrder = function () {
                orderSvc.getListOrder().then(function (response) {
                    $scope.orders = response.data;
                    console.log($scope.orders);
                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }
            getListOrder();

            $scope.search = function () {
                getListOrder();
            }

            $scope.print = function(){
                window.print();
            }
        }
	]);