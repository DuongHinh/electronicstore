angular.module('electronicStoreApp.global.services.productCategories', ['electronicStoreApp.global.services'])
    .service('productCategoriesSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getList = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/productCategories/getall?keyword=' + keyword + '&skip=' + skip + '&pageSize=' + pageSize);
            }

            this.getAllCategories = function (keyword) {
                return apiSvc.get('/api/productCategories/getall?keyword=' + keyword);
            }

            this.addNew = function (data) {
                return apiSvc.post('api/productCategories/create', data);
            }

            this.update= function (data) {
                return apiSvc.put('api/productCategories/update', data);
            }

            this.delete = function (id) {
                return apiSvc.delete('api/productCategories/delete?id=' + id);
            }

            this.deleteMulti = function (listCategoryIds) {
                return apiSvc.delete('api/productCategories/deletemulti?listCategoryIds=' + listCategoryIds);
            }

            this.getById = function (id) {
                return apiSvc.get('api/productCategories/getbyid?id=' + id);
            }
        }
    ])