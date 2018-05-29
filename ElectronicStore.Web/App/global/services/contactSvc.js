angular.module('electronicStoreApp.services.contact', ['electronicStoreApp.global.services'])
    .service('contactSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getContactInfo = function () {
                return apiSvc.get('/api/contact/getContactInfo?');
            }

            this.updateContactInfo = function (data) {
                return apiSvc.post('api/contact/update', data);
            }
        }
    ])