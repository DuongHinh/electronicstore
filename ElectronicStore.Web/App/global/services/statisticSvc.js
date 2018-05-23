angular.module('electronicStoreApp.services.statistics', ['electronicStoreApp.global.services'])
    .service('statisticSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getProfitAndRevenue = function (date) {
                return apiSvc.get('/api/statistic/GetProfitAndRevenue?date=' + date);
            }

            this.getHomePageStaticstic = function () {
                return apiSvc.get('/api/statistic/GetHomePageStaticstic?');
            }
        }
    ])