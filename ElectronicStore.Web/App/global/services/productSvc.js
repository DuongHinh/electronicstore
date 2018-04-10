angular.module('meritApp.global.services.products', ['electronicStoreApp.global'])
    .service('productSvc',
    [
        '$q', '$timeout', '$http', '$modal', 'crudSvc',
        function ($q, $timeout, $http, $modal, crudSvc) {

            this.getListProduct = function(params){
                crudSvc.get('/api/product/getall', params);
            }; 
        }
    ])