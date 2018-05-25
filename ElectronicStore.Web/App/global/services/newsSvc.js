angular.module('electronicStoreApp.services.news', ['electronicStoreApp.global.services'])
    .service('newsSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListNews = function (keyword) {
                return apiSvc.get('/api/news/getall?keyword=' + keyword);
            }

            this.getNewsById = function (id) {
                return apiSvc.get('api/news/getbyid?id=' + id);
            }

            this.addNews = function (data) {
                return apiSvc.post('api/news/create', data);
            }

            this.updateNews = function (data) {
                return apiSvc.put('api/news/update', data);
            }

            this.deleteNews = function (id) {
                return apiSvc.delete('api/news/delete?id=' + id);
            }
        }
    ])