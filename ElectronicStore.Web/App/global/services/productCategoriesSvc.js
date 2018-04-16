angular.module('electronicStoreApp.global.services.productCategories', ['electronicStoreApp.global.services'])
    .service('productCategoriesSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListProductCategories = function (keyword) {
                return apiSvc.get('/api/productCategories/getall?keyword=' + keyword);
            }

            this.addNewProductCategories = function (data) {
                return apiSvc.post('api/productCategories/create', data);
            }
        }
    ])