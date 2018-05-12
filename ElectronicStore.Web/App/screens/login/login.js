angular.module('electronicStoreApp.screens.login',
	[
		'ui.router', 'electronicStoreApp.global.services.auth'
	])
	.config(function ($stateProvider) {
	    $stateProvider.state('login', {
	        url: "/login",
	        templateUrl: "/app/screens/login/login.html",
	    })

	})
	.controller('loginController',
	[
		'$state', '$rootScope', '$scope', '$stateParams', 'authSvc', '$injector',
		function ($state, $rootScope, $scope, $stateParams, authSvc, $injector) {
		    $scope.loginData = {
		        username: "",
		        password: ""
		    };

		    $scope.login = function () {
		        authSvc.login($scope.loginData.username, $scope.loginData.password).then(function (response) {
		            if (response != null && response.error != undefined) {
		                console.log(response.error_description);
		            }
		            else {
		                $injector.get("$state").go("home");
		            }
		        });
		    }

		    //$scope.login = function () {
		    //    $state.go('home', {}, { reload: true });
		    //}
		}
	]);