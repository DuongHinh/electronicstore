angular.module('electronicStoreApp.services.orders', ['electronicStoreApp.global.services'])
    .service('orderSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListOrder = function () {
                return apiSvc.get('/api/order/getall?keyword=');
            }
        }
    ])