angular.module('electronicStoreApp.screens.productCategories',
	[
		'ui.router'
	])
	.config(function ($stateProvider) {

	    $stateProvider
			.state('productCategories', {
			    url: "/productCategories",
			    parent: 'layout',
			    templateUrl: "/app/screens/productCategories/list.html"
			});

	    $stateProvider
			.state('productCategories.new', {
			    url: "/productCategories/new",
			    parent: 'layout',
			    templateUrl: "app/screens/productCategories/new.html"
			});

	    $stateProvider
			.state('productCategories.edit', {
			    url: "/productCategories/new",
			    parent: 'layout',
			    templateUrl: "app/screens/productCategories/edit.html"
			});

	})
	.controller('productCategoriesListController',
	[
		'$scope', '$state', '$stateParams', '$rootScope',
		function ($scope, $state, $stateParams, $rootScope) {
		    $scope.title = 'Product Categories List';
		    //$scope.loading = false;
		    //$scope.getProducts = function (page) {
		    //    page = page || 0;
		    //    var  params= {
		    //            keyword: $scope.keyword,
		    //            skip: ,
		    //            pageSize: 16
		    //        }
		    //    $scope.loading = true;
		    //    productSvc.getListProduct(params, function (response) {
		    //        $scope.products = response.data.Items;
		    //        $scope.page = response.data.Page;
		    //        $scope.pagesCount = response.data.TotalPages;
		    //        $scope.totalCount = response.data.TotalCount;
		    //        $scope.loading = false;
		    //    }, function (error) {
		    //        $scope.loading = false;
		    //        console.log('Load product failed.');
		    //    });
		    //}
		}
	])
	.controller('productCategoriesNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope',
		function ($scope, $state, $log, $stateParams, $rootScope) {


		}
	])
    .controller('productCategoriesEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope',
		function ($scope, $state, $log, $stateParams, $rootScope) {

		}
	]);