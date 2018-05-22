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
	    $stateProvider
			.state('orders.detail', {
			    url: "/orders/detail/{id}",
			    parent: 'layout',
			    templateUrl: "/app/screens/orders/detail.html"
			});

	})
	.controller('orderListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'orderSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, orderSvc) {

            $scope.title = 'Quản lý đơn hàng';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10

            $scope.statusArr = [
                { id: 0, value: "Đang xử lý" },
                { id: 1, value: "Đã xong" },
                { id: 2, value: "Hủy" }
            ];

            $scope.orderStatus = null;

            var getListOrder = function () {
                if ($scope.orderStatus === undefined || $scope.orderStatus === '') {
                    $scope.orderStatus = null;
                }
                orderSvc.getListOrder($scope.keyword, $scope.orderStatus).then(function (response) {
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
	])
    .controller('orderDetailController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'orderSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, orderSvc) {

            $scope.title = 'Chi tiết đơn hàng';
            $scope.loading = true;
            
            var loadOrderDetail = function () {
                orderSvc.getOrderDetail(parseInt($stateParams.id)).then(function (response) {
                    $scope.order = response.data;
                    console.log($scope.order);
                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }

            loadOrderDetail();

            $scope.paymentStatusArr = [
                { id: 0, value: "Chưa thanh toán" },
                { id: 1, value: "Đã thanh toán" },
                { id: 2, value: "Hủy" }
            ];

            $scope.shipStatusArr = [
                { id: 0, value: "Chưa giao hàng" },
                { id: 1, value: "Đang giao hàng" },
                { id: 2, value: "Đã giao hàng" },
                { id: 3, value: "Huỷ" }
            ];

            $scope.statusArr = [
                { id: 0, value: "Đang xử lý" },
                { id: 1, value: "Đã xong" },
                { id: 2, value: "Hủy" }
            ];


            $scope.updateOrderStatus = function () {
                var command = {
                    Id: $scope.order.OrderId,
                    ShipDate: $scope.order.ShipDate,
                    PaymentStatus: $scope.order.PaymentStatus,
                    ShipStatus: $scope.order.ShipStatus,
                    Status: $scope.order.Status
                }
                orderSvc.updateOrderStatus(command).then(function (response) {
                    alert('Cập nhật trạng thái thành công!');
                    $state.go('orders');
                }, function (error) {
                    console.log(error);
                });
            }
            

            $scope.print = function(){
                window.print();
            }
        }
	])
    .filter('paymentFilter', function () {
        return function (input) {
            if (input == 0)
                return 'Chưa thanh toán';
            else if (input == 1)
                return 'Đã thanh toán';
            else if (input == 2)
                return 'Huỷ';
            else
                return 'Không xác định';
        }
    }).filter('shipFilter', function () {
        return function (input) {
            if (input == 0)
                return 'Chưa giao hàng';
            else if (input == 1)
                return 'Đang giao hàng';
            else if (input == 2)
                return 'Đã giao hàng';
            else if (input == 3)
                return 'Hủy';
            else
                return 'Không xác định'
        }
    }).filter('statusFilter', function () {
        return function (input) {
            if (input == 0)
                return 'Đang xử lý';
            else if (input == 1)
                return 'Đã xong';
            else if (input == 2)
                return 'Hủy';
            else
                return 'Không xác định'
        }
    });