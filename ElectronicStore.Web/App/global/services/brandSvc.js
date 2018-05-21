angular.module('electronicStoreApp.services.brands', ['electronicStoreApp.global.services'])
    .service('brandSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListBrand = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/brands/getall?keyword=' + keyword + '&skip=' + skip + '&pageSize=' + pageSize);
            }

            this.getBrandById = function (id) {
                return apiSvc.get('api/brands/getbyid?id=' + id);
            }

            this.addNewBrand = function (data) {
                return apiSvc.post('api/brands/create', data);
            }

            this.updateBrand = function (data) {
                return apiSvc.put('api/brands/update', data);
            }

            this.deleteBrand = function (id) {
                return apiSvc.delete('api/brands/delete?id=' + id);
            }
        }
    ])