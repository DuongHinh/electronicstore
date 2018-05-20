angular.module('electronicStoreApp.services.roles', ['electronicStoreApp.global.services'])
    .service('roleSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListRole = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/role/FindAllRolePaging?keyword=' + keyword + '&skip=' + skip + '&pageSize=' + pageSize);
            }

            this.getAll = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/role/FindAllRole?');
            }

            this.addNewRole = function (data) {
                return apiSvc.post('api/role/create', data);
            }

            this.updateRole = function (data) {
                return apiSvc.put('api/role/update', data);
            }

            this.deleteRole = function (id) {
                return apiSvc.delete('api/role/delete?id=' + id);
            }

            //this.deleteMultiProduct = function (listProductIds) {
            //    return apiSvc.delete('api/product/deletemulti?listProductIds=' + listProductIds);
            //}

            this.getRoleById = function (id) {
                return apiSvc.get('api/role/getbyid?id=' + id);
            }
        }
    ])