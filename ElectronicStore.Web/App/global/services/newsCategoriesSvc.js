angular.module('electronicStoreApp.services.newsCategories', ['electronicStoreApp.global.services'])
    .service('newsCategoriesSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListNewsCategory = function (keyword) {
                return apiSvc.get('/api/newsCategories/getall?keyword=' + keyword);
            }

            this.getNewsCategoryById = function (id) {
                return apiSvc.get('api/newsCategories/getbyid?id=' + id);
            }

            this.addNewsCategory = function (data) {
                return apiSvc.post('api/newsCategories/create', data);
            }

            this.updateNewsCategory = function (data) {
                return apiSvc.put('api/newsCategories/update', data);
            }

            this.deleteNewsCategory = function (id) {
                return apiSvc.delete('api/newsCategories/delete?id=' + id);
            }
        }
    ])