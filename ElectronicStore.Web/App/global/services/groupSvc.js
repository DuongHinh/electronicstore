angular.module('electronicStoreApp.services.groups', ['electronicStoreApp.global.services'])
    .service('groupSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListGroup = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/group/findallgroup?keyword=' + keyword + '&skip=' + skip + '&pageSize=' + pageSize);
            }

            this.getAll = function (keyword) {
                return apiSvc.get('/api/group/getall?');
            }

            this.addNewGroup = function (data) {
                return apiSvc.post('api/group/create', data);
            }

            this.updateGroup = function (data) {
                return apiSvc.put('api/group/update', data);
            }

            this.deleteGroup = function (id) {
                return apiSvc.delete('api/group/delete?id=' + id);
            }

            //this.deleteMultiProduct = function (listProductIds) {
            //    return apiSvc.delete('api/product/deletemulti?listProductIds=' + listProductIds);
            //}

            this.getGroupById = function (id) {
                return apiSvc.get('api/group/getbyid?id=' + id);
            }
        }
    ])