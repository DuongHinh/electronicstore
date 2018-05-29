angular.module('electronicStoreApp.screens.about',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.services.about',
        'angularUtils.directives.dirPagination',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common',
        'ngSanitize',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.bootstrap.modal'
	])
	.config(function ($stateProvider) {

	    $stateProvider
			.state('about-us', {
			    url: "/about-us",
			    parent: 'layout',
			    templateUrl: "/app/screens/systems/about/about.html"
			});
	})
	.controller('aboutController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter', 'aboutSvc',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter, aboutSvc) {

            $scope.title = 'Thông tin giới thiệu';

            $scope.about = {};

            var loadData = function () {
                aboutSvc.getContactInfo().then(function (response) {
                    $scope.about = response.data;

                }, function (response) {
                    console.log(response);
                });
            }

            loadData();

            $scope.update = function () {
                aboutSvc.updateContactInfo($scope.about).then(function (response) {
                    $state.go($state.current, {}, { reload: true });
                }, function (response) {
                    console.log(response);
                });
            }
        }
	]);