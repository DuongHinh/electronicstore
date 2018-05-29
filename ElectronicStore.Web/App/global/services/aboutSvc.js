angular.module('electronicStoreApp.services.about', ['electronicStoreApp.global.services'])
    .service('aboutSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getContactInfo = function () {
                return apiSvc.get('/api/about/getAboutInfo?');
            }

            this.updateContactInfo = function (data) {
                return apiSvc.post('api/about/update', data);
            }
        }
    ])