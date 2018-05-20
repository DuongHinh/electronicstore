﻿angular.module('electronicStoreApp.screens.products',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.services.feedbacks',
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
			.state('feedbacks', {
			    url: "/feedbacks",
			    parent: 'layout',
			    templateUrl: "/app/screens/systems/feedbacks/feedbacks.html"
			});
	})
	.controller('feedbackListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'feedbackSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, feedbackSvc) {

            $scope.title = 'Phản hồi của khách hàng';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;
            $scope.totalPages = 0;
            $scope.currentPage = 1
          
            var getListFeedback = function () {
                var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
                feedbackSvc.getListFeedback($scope.keyword, skip, $scope.itemsPerPage).then(function (response) {
                    $scope.feedbacks = response.data.Results;
                    $scope.totalPages = response.data.TotalPages;
                    $scope.currentPage = response.data.CurrentPage;
                    $scope.totalItems = response.data.TotalResults;
                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }
            getListFeedback();

            $scope.search = function () {
                getListFeedback();
            }
        }
	]);