angular.module('electronicStoreApp.screens.products',
	[
		'ui.router'
	])
	.config(function($stateProvider) {

		$stateProvider
			.state('products', {
			    url: "/products",
			    parent: 'layout',
			    templateUrl: "/app/screens/product/list.html"
			});

		$stateProvider
			.state('products.new', {
			    url: "/products/new",
			    parent: 'layout',
			    templateUrl: "app/screens/product/new.html"
			});

		$stateProvider
			.state('products.edit', {
			    url: "/products/new",
				parent: 'layout',
				templateUrl: "app/screens/product/edit.html"
			});

	})
	.controller('productListController',
	[
		'$scope', '$state', '$stateParams', '$rootScope',
		function ($scope, $state, $stateParams, $rootScope) {
		    $scope.title = 'Product List';
		    $scope.loading = false;
		    $scope.getProducts = function (page) {
		        page = page || 0;
		        var  params= {
		                keyword: $scope.keyword,
		                skip: 0,
		                pageSize: 16
		            }
		        $scope.loading = true;
		        productSvc.getListProduct(params, function (response) {
		            $scope.products = response.data.Items;
		            $scope.page = response.data.Page;
		            $scope.pagesCount = response.data.TotalPages;
		            $scope.totalCount = response.data.TotalCount;
		            $scope.loading = false;
		        }, function (error) {
		            $scope.loading = false;
		            console.log('Load product failed.');
		        });
		    }
		}
	])
	.controller('productNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope',
		function ($scope, $state, $log, $stateParams, $rootScope) {

			
		}
	])
    .controller('productEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope',
		function ($scope, $state, $log, $stateParams, $rootScope) {

		}
	]);