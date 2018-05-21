angular.module('electronicStoreApp.services.feedbacks', ['electronicStoreApp.global.services'])
    .service('feedbackSvc',
    [
        '$q', '$timeout', '$http', 'apiSvc',
        function ($q, $timeout, $http, apiSvc) {

            this.getListFeedback = function (keyword, skip, pageSize) {
                return apiSvc.get('/api/feedbacks/getall?keyword=' + keyword + '&skip=' + skip + '&pageSize=' + pageSize);
            }

            this.getFeedbackById = function (id) {
                return apiSvc.get('api/feedbacks/getbyid?id=' + id);
            }
        }
    ])