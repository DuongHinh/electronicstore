angular.module('electronicStoreApp.services.orders', ['electronicStoreApp.global.services'])
    .service('orderSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListOrder = function (keyword, status) {
                return apiSvc.get('/api/order/getall?keyword=' + keyword + '&status=' + status);
            }

            this.getOrderDetail = function (orderId) {
                return apiSvc.get('/api/order/getdetail?orderId=' + orderId);
            }

            this.updateOrderStatus = function (data) {
                return apiSvc.put('/api/order/updateOrderStatus', data);
            }
        }
    ])