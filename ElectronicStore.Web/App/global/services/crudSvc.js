angular.module('electronicStoreApp.global.services', [])
.service('crudSvc', ['$http', function ($http) {

    this.post = function (url, data, success, failure) {
        return $http.post(url, data).then(function (result) {
            success(result);
        }, function (error) {
            console.log(error.status)
        });
    }

    this.put = function (url, data, success, failure) {
        return $http.put(url, data).then(function (result) {
            success(result);
        }, function (error) {
            console.log(error.status)
        });
    }

    this.get = function(url, params, success, failure) {
        return $http.get(url, params).then(function (result) {
            success(result);
        }, function (error) {
            failure(error);
        });
    }

    this.delete = function(url, data, success, failure) {
        return $http.delete(url, data).then(function (result) {
            success(result);
        }, function (error) {
            console.log(error.status)
        });
    }
}]);