angular.module('electronicStoreApp.global.services.products', ['electronicStoreApp.global.services'])
    .service('productSvc',
    [
        '$q', '$timeout', '$http', '$modal', 'crudSvc',
        function ($q, $timeout, $http, $modal, crudSvc) {

            this.getListProduct = function(params){
                crudSvc.get('/api/product/getall', params);
            }; 
        }
    ])