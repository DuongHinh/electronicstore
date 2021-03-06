﻿angular.module('electronicStoreApp.screens.login',
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

		    $scope.errorMessage = null;

		    $scope.working = false;
		    $scope.login = function () {
		        authSvc.login($scope.loginData.username, $scope.loginData.password).then(function (response) {
		            $state.go('home', {}, { reload: true });
		            $scope.working = true;
		        }, function (response) {
		            $scope.working = false;
		            if (response.error == "invalid_grant") {
		                $scope.errorMessage = "Tài khoản hoặc mật khẩu không đúng";
		            }
		            if (response.error == "invalid_role") {
		                $scope.errorMessage = "Bạn không có quyền truy cập";
		            }
		        });
		    }
		}
	]);