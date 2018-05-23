angular.module('electronicStoreApp.screens.home',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.services.statistics',
        'electronicStoreApp.global.common',
        'ngSanitize',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.bootstrap.modal'
	])
	.config(function ($stateProvider) {
	    $stateProvider.state('home', {
	        url: "/home",
	        templateUrl: "/app/screens/home/home.html",
	        parent: 'layout',
	    })
	})
	.controller('homeController',
	[
		'$state', '$rootScope', '$scope', '$stateParams', '$q', '$window', 'apiSvc', 'statisticSvc',
		function ($state, $rootScope, $scope, $stateParams, $q, $window, apiSvc, statisticSvc) {

		    var validateRequest = function () {
		        var deferred = $q.defer();
		        var url = 'api/home/redirectRequest';
		        apiSvc.get(url).then(function (response) {
		            deferred.resolve(response);
		        }, function (error) {
		            console.log(error);
		            deferred.reject(error);
		        });
		        return deferred.promise;
		    }

		    validateRequest();

		    var loadHomePageStaticstic = function () {
		        statisticSvc.getHomePageStaticstic().then(function (response) {
		            $scope.dataStaticstic = response.data;
		            console.log($scope.dataStaticstic);
		        },
                function (error) {
                    console.log(error);
                });
		    }
		  
		    var init = function ()
		    {
		        loadHomePageStaticstic();
		    }

		    init();
        }
	]);