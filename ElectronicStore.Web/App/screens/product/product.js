angular.module('electronicStoreApp.screens.products',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.global.services.productCategories',
        'angularUtils.directives.dirPagination',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common'
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
			    url: "/products/edit/{id}",
				parent: 'layout',
				templateUrl: "app/screens/product/edit.html"
			});

	})
	.controller('productListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter) {
		    $scope.title = 'Product list';
		    $scope.loading = true;
		    $scope.keyword = '';
		    $scope.itemsPerPage = 16;
		    $scope.currentPage = 1;
		    $scope.totalItems = 0;

		    $scope.selectedProductIds = [];

		    var getListProduct = function () {
		        var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
		        productSvc.getListProduct($scope.keyword).then(function (response) {
		            $scope.products = response.data;
		            //$scope.page = response.data.CurrentPage - 1;
		            //$scope.pagesCount = response.data.TotalPages;
		            //$scope.totalCount = response.data.TotalResults;
		            $scope.loading = false;
		        }, function (error) {
		            $scope.loading = false;
		            console.log(error);
		        });
		    }
		    getListProduct();

		    $scope.search = function () {
		        getListProduct();
		    }

		    $scope.deleteProduct = function (item) {
		        $ngBootbox.confirm("Are you sure delete " + item.Name + " ?").then(function () {
		            var id = item.Id;
		            productSvc.deleteProduct(id).then(function (response) {
		                getListProduct();
		            }, function (error) {
		                console.log(error);
		            });
		        });
		    }

		    $scope.selectAllProduct = function () {
		        if ($scope.selectedProductIds.length === $scope.products.length) {
		            $scope.selectedProductIds = [];
		        } else {
		            // get all id of selectedProducts
		            $scope.selectedProductIds = _.pluck($scope.products, 'Id');
		        }
		    }

		    $scope.productSelected = function (product) {
		        return $scope.selectedProductIds.indexOf(product.Id) !== -1;
		    };

		    $scope.selectProduct = function (product) {
		        var index = $scope.selectedProductIds.indexOf(product.Id);
		        if (index === -1) {
		            // add
		            $scope.selectedProductIds.push(product.Id);
		        } else {
		            // remove
		            $scope.selectedProductIds.splice(index, 1);
		        }
		    };

		    $scope.deleteMultiProduct = function() {		       
		        $ngBootbox.confirm("Are you sure delete " + $scope.selectedProductIds.length + " product?").then(function () {
		            var listProductIds = $scope.selectedProductIds.join();
		            productSvc.deleteMultiProduct(listProductIds).then(function (response) {
		                getListProduct();
		            }, function (error) {
		                console.log(error);
		            });		           
		        });
		    }

		}
	])
	.controller('productNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc) {
		    $scope.title = 'Create new product';
		    $scope.submitted = false;
		    $scope.product = {
		        Status: true,
		        Name: '',
		        Alias: '',
		        Quantity: 0,
		        OriginalPrice: null,
		        Price: null,
		        ProductCategory: null
		    }

		    $scope.addNewProduct = function () {
		        $scope.submitted = true;
		        if ($scope.product.Name === '' || $scope.product.Name === null || $scope.product.Name.length > 256) {
		            return;
		        }

		        if ($scope.product.Alias === '' || $scope.product.Alias === null || $scope.product.Alias.length > 256) {
		            return;
		        }

		        if ($scope.product.OriginalPrice === null || $scope.product.Price === null) {
		            return;
		        }


		        if ($scope.product.CategoryId === null) {
		            return;
		        }

		        productSvc.addNewProduct($scope.product).then(function (record) {
		            alert('Add new product success!');
		            $state.go('products');
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.product.Alias = commonSvc.getAlias(input);
		    }

		    var loadProductCategories = function () {
		        productCategoriesSvc.getListProductCategories('').then(function (response) {
		            $scope.productCategories = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    loadProductCategories();
			
		}
	])
    .controller('productEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc) {
		    $scope.title = 'Update product';
		    $scope.submitted = false;
		    $scope.getAlias = function (input) {
		        $scope.product.Alias = commonSvc.getAlias(input);
		    }

		    var loadProductDetail = function() {
		        productSvc.getProductById(parseInt($stateParams.id)).then(function (response) {
		            $scope.product = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    loadProductDetail();

		    var loadProductCategories = function () {
		        productCategoriesSvc.getListProductCategories('').then(function (response) {
		            $scope.productCategories = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    loadProductCategories();

		    $scope.updateProduct = function () {
		        $scope.submitted = true;
		        if ($scope.product.Name === '' || $scope.product.Name === null || $scope.product.Name.length > 256) {
		            return;
		        }

		        if ($scope.product.Alias === '' || $scope.product.Alias === null || $scope.product.Alias.length > 256) {
		            return;
		        }

		        if ($scope.product.OriginalPrice === null || $scope.product.Price === null) {
		            return;
		        }


		        if ($scope.product.CategoryId === null) {
		            return;
		        }
		        productSvc.updateProduct($scope.product).then(function (record) {
		            alert('Update product success!');
		            $state.go('products');
		        }, function (error) {
		            console.log(error);
		        });
		    }
		}
	]);