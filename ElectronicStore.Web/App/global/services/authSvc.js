angular.module('electronicStoreApp.global.services.auth', ['electronicStoreApp.global', 'electronicStoreApp.global.common'])
    .service('authTokenSvc', [
		'localStorageService', '$http', '$q', '$window', '$injector', 'authData',
		function (localStorageService, $http, $q, $window, $injector, authData) {

		    var setToken = function (token) {
		        localStorageService.set("oauth_token", token);
		    };

		    var getToken = function () {
		        return localStorageService.get("oauth_token");
		    };

		    var clearToken = function () {
		        console.log('clearing token');
		        return localStorageService.remove("oauth_token");
		    };

		    this.getToken = function () {
		        return getToken();
		    };

		    this.setToken = function (token) {
		        setToken(token);
		    };

		    this.clearToken = function () {
		        clearToken();
		    };

		   
		    this.setHeader = function () {
		        delete $http.defaults.headers.common['X-Requested-With'];
		        if ((authData.authInformation != undefined)
                    && (authData.authInformation.accessToken != undefined)
                    && (authData.authInformation.accessToken != null)
                    && (authData.authInformation.accessToken != "")) {
		            $http.defaults.headers.common['Authorization'] = 'Bearer ' + authData.authInformation.accessToken;
		            $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
		        }
		        else {
		            var stateService = $injector.get('$state');
		            $injector.get("$state").go("login");
		        }
		    }

		    var init = function () {
		        var tokenInfo = getToken();
		        if (tokenInfo) {
		            authData.authInformation.isAuthorized = true;
		            authData.authInformation.username = tokenInfo.username;
		            authData.authInformation.accessToken = tokenInfo.accessToken
		        }
		    }

		    init();

		    //this.redirectRequest = function () {
		    //    var url = 'api/home/RedirectRequest';
		    //    var deferred = $q.defer();
		    //    $http.get(url).then(function (response) {
		    //        deferred.resolve(response);
		    //    }, function (error) {
		    //        deferred.reject(error);
		    //    });
		    //    return deferred.promise;
		    //}
		}
    ])
   .service('authSvc', [
		'$http', 'authTokenSvc', '$q', '$rootScope', '$injector', 'authData',
		function ($http, authTokenSvc, $q, $rootScope, $injector, authData) {

		    var logout = function () {
		        authTokenSvc.clearToken();
		        authData.authInformation.isAuthorized = false;
		        authData.authInformation.username = "";
		        authData.authInformation.accessToken = null;
		        $injector.get("$state").go("login");
		    };

		    this.logout = logout;

		    this.login = function (username, password) {
		        var authRequest = {
		            username: username,
		            password: password,
		            grant_type: 'password'
		        };
		        var deferred = $q.defer();

		        //console.log("direct login requested");

		        $http.post('/oauth/token', $.param(authRequest), {
		            headers: {
		                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
		            }
		        }).success(function (token) {
		            userInfo = {
		                accessToken: token.access_token,
		                username: username
		            };

		            authTokenSvc.setToken(userInfo);
		            authData.authInformation.isAuthorized = true;
		            authData.authInformation.username = username;
		            authData.authInformation.accessToken = userInfo.accessToken;
		            deferred.resolve(token);
		        }).error(function (error) {
		            //logout();
		            authData.authInformation.isAuthorized = false;
		            authData.authInformation.username = "";
		            authData.authInformation.accessToken = null;
		            deferred.reject(error);
		        });

		        return deferred.promise;
		    };
		}
])
.factory('authData', [function () {
    var authDataFactory = {};

    var authentication = {
        isAuthorized: false,
        username: "",
        accessToken : null
    };
    authDataFactory.authInformation = authentication;

    return authDataFactory;
}]);