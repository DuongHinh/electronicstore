angular.module('electronicStoreApp.services.users', ['electronicStoreApp.global.services'])
    .service('userSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListUsers = function (keyword) {
                return apiSvc.get('/api/user/getall?keyword=' + keyword);
            }

            this.addNewUser = function (data) {
                return apiSvc.post('api/user/create', data);
            }

            this.updateUser = function (data) {
                return apiSvc.put('api/user/update', data);
            }

            this.deleteUser = function (id) {
                return apiSvc.delete('api/user/delete?id=' + id);
            }

            //this.deleteMultiProduct = function (listProductIds) {
            //    return apiSvc.delete('api/product/deletemulti?listProductIds=' + listProductIds);
            //}

            this.getUserById = function (id) {
                return apiSvc.get('api/user/getbyid?id=' + id);
            }
        }
    ])