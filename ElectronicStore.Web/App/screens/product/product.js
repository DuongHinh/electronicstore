angular.module('electronicStoreApp.screens.products',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.global.services.productCategories',
        'electronicStoreApp.services.brands',
        'angularUtils.directives.dirPagination',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common',
        'ngSanitize',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.bootstrap.modal'
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
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'brandSvc', '$ngBootbox', '$filter',
        function ($scope, $state, $stateParams, $rootScope, productSvc, productCategoriesSvc, brandSvc, $ngBootbox, $filter) {

		    $scope.title = 'Danh sách sản phẩm';
		    $scope.loading = true;
		    $scope.keyword = '';
		    $scope.selectedCategoryId = null;
		    $scope.selectedBrandId = null;
		    $scope.itemsPerPage = 10;
		    $scope.totalPages = 0;
		    $scope.currentPage = 1

		    $scope.selectedProductIds = [];

		    var loadProductCategories = function () {
		        productCategoriesSvc.getAllCategories('').then(function (response) {
		            $scope.productCategories = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    var loadBrands = function () {
		        brandSvc.getAll().then(function (response) {
		            $scope.brands = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    var init = function () {
		        loadProductCategories();
		        loadBrands();
		    }

		    init();

		    var getListProduct = function () {
		        var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
		        productSvc.getListProduct($scope.keyword, $scope.selectedCategoryId, $scope.selectedBrandId, skip, $scope.itemsPerPage).then(function (response) {
		            $scope.products = response.data.Results;
		            $scope.totalPages = response.data.TotalPages;
		            $scope.currentPage = response.data.CurrentPage;
		            $scope.totalItems = response.data.TotalResults;

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
		        $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.Name + " ?").then(function () {
		            var id = item.Id;
		            productSvc.deleteProduct(id).then(function (response) {
		                $state.reload();
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
		        $ngBootbox.confirm("Bạn có muốn xóa " + $scope.selectedProductIds.length + " sản phẩm?").then(function () {
		            var listProductIds = $scope.selectedProductIds.join();
		            productSvc.deleteMultiProduct(listProductIds).then(function (response) {
		                $state.reload();
		            }, function (error) {
		                console.log(error);
		            });		           
		        });
		    }

		    $scope.print = function () {
		        window.print();
		    }
		}
	])
	.controller('productNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc', 'brandSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc, brandSvc) {

		    $scope.title = 'Thêm mới sản phẩm';
		    $scope.submitted = false;
		    $scope.moreImages = [];


		    var loadProductCategories = function () {
		        productCategoriesSvc.getAllCategories('').then(function (response) {
		            $scope.productCategories = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }
		   
		    var loadBrands = function () {
		        brandSvc.getAll().then(function (response) {
		            $scope.brands = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }



		    var init = function () {
		        loadProductCategories();
		        loadBrands();
		    }

		    init();

		    $scope.product = {
		        Status: true,
		        Name: '',
		        Alias: '',
		        Quantity: 0,
		        OriginalPrice: null,
		        Price: null,
		        ProductCategory: null,
		        Image : ""
		    }

		    $scope.$on("fileProgress", function (e, progress) {
		        $scope.progress = progress.loaded / progress.total;
		    });

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.product.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    $scope.chooseMoreImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.moreImages.push(fileUrl);
		            })
		        }
		        finder.popup();
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

		        if ($scope.product.BrandId === null) {
		            return;
		        }


		        $scope.product.MoreImages = angular.toJson($scope.moreImages);

		        productSvc.addNewProduct($scope.product).then(function (record) {
		            alert('Thêm mới sản phẩm thành công!');
		            $state.go('products');
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.product.Alias = commonSvc.getAlias(input);
		    }	
		}
	])
    .controller('productEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc', 'brandSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc, brandSvc) {

		    $scope.title = 'Cập nhập sản phẩm';
		    $scope.submitted = false;
		    $scope.moreImages = [];

		    $scope.getAlias = function (input) {
		        $scope.product.Alias = commonSvc.getAlias(input);
		    }

		    var loadProductDetail = function() {
		        productSvc.getProductById(parseInt($stateParams.id)).then(function (response) {
		            $scope.product = response.data;
		            $scope.moreImages = angular.fromJson($scope.product.MoreImages);
		        }, function (error) {
		            console.log(error);
		        });
		    }


		    var loadProductCategories = function () {
		        productCategoriesSvc.getAllCategories('').then(function (response) {
		            $scope.productCategories = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }
	    

		    var loadBrands = function () {
		        brandSvc.getAll().then(function (response) {
		            $scope.brands = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    var init = function () {
		        loadProductDetail();
		        loadProductCategories();
		        loadBrands();
		    }

		    init();

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.product.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    $scope.chooseMoreImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.moreImages.push(fileUrl);
		            })
		        }
		        finder.popup();
		    }

		    $scope.updateProduct = function () {
		        $scope.submitted = true;
		        if ($scope.product.Name === undefined || $scope.product.Name === '' || $scope.product.Name === null || $scope.product.Name.length > 256) {
		            return;
		        }

		        if ($scope.product.Alias === undefined || $scope.product.Alias === '' || $scope.product.Alias === null || $scope.product.Alias.length > 256) {
		            return;
		        }

		        if ($scope.product.OriginalPrice === null || $scope.product.Price === null) {
		            return;
		        }


		        if ($scope.product.CategoryId === null) {
		            return;
		        }

		        $scope.product.MoreImages = angular.toJson($scope.moreImages);

		        productSvc.updateProduct($scope.product).then(function (record) {
		            alert('Cập nhật sản phẩm thành công!');
		            $state.go('products');
		        }, function (error) {
		            console.log(error);
		        });
		    }
		}
	]);