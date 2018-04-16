angular.module('electronicStoreApp.global.services.products', ['electronicStoreApp.global.services'])
    .service('productSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListProduct = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/product/getall?keyword=' + keyword + '&skip=' + skip + '&pageSize=' + pageSize);
            }

            //this.getListProduct = function (keyword) {
            //    return apiSvc.get('/api/product/getall?keyword=' + keyword);
            //}

            this.addNewProduct = function (data) {
                return apiSvc.post('api/product/create', data);
            }
            
            this.updateProduct = function (data) {
                return apiSvc.put('api/product/update', data);
            }

            this.deleteProduct = function (id) {
                return apiSvc.delete('api/product/delete?id=' + id);
            }

            this.deleteMultiProduct = function (listProductIds) {
                return apiSvc.delete('api/product/deletemulti?listProductIds=' + listProductIds);
            }

            this.getProductById = function (id) {
                return apiSvc.get('api/product/getbyid?id=' + id);
            }
        }
    ])