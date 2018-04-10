angular.module('electronicStoreApp.screens.login',
	[
		'ui.router'
	])
	.config(function ($stateProvider) {
	    $stateProvider.state('login', {
	        url: "/login",
	        templateUrl: "/app/screens/login/login.html",
	    })

	})
	.controller('loginController',
	[
		'$state', '$rootScope', '$scope', '$stateParams',
		function ($state, $rootScope, $scope, $stateParams) {
          

		    $scope.login = function () {
		        $state.go('home', { reload: true });
		    }
		}
	]);