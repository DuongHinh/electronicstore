angular.module('electronicStoreApp.screens.news',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.global.services.productCategories',
        'electronicStoreApp.services.news',
        'electronicStoreApp.services.brands',
        'electronicStoreApp.services.newsCategories',
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
			.state('news', {
			    url: "/news",
			    parent: 'layout',
			    templateUrl: "/app/screens/news/list.html"
			});

	    $stateProvider
			.state('news.new', {
			    url: "/news/new",
			    parent: 'layout',
			    templateUrl: "app/screens/news/new.html"
			});

	    $stateProvider
			.state('news.edit', {
			    url: "/news/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/news/edit.html"
			});

	})
	.controller('newsListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'newsCategoriesSvc', 'newsSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, newsCategoriesSvc, newsSvc) {

            $scope.title = 'Danh sách tin tức';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;


            var getListNews = function () {
                newsSvc.getListNews($scope.keyword).then(function (response) {
                    $scope.newses = response.data;

                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }

            getListNews();

            $scope.search = function () {
                getListNews();
            }

            $scope.deleteNews = function (item) {
                $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.Name + " ?").then(function () {
                    var id = item.Id;
                    newsSvc.deleteNews(id).then(function (response) {
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
.controller('newsAddController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productCategoriesSvc', 'commonSvc', 'newsCategoriesSvc', 'newsSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productCategoriesSvc, commonSvc, newsCategoriesSvc, newsSvc) {
		    $scope.title = 'Thêm mới tin tức';
		    $scope.submitted = false;


		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.news.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    var loadCategory = function () {
		        newsCategoriesSvc.getListNewsCategory("").then(function (response) {
		            $scope.newsCategories = response.data;
		            $scope.loading = false;
		        }, function (error) {
		            $scope.loading = false;
		            console.log(error);
		        });
		    }

		    loadCategory();


		    $scope.addNews = function () {
		        $scope.submitted = true;
		        if ($scope.news.Title === '' || $scope.news.Title === null || $scope.news.Title.length > 256) {
		            return;
		        }

		        if ($scope.news.Alias === '' || $scope.news.Alias === null || $scope.news.Alias.length > 256) {
		            return;
		        }

		        if ($scope.news.CategoryId === null || $scope.news.CategoryId == undefined) {
		            return;
		        }

		        newsSvc.addNews($scope.news).then(function (record) {
		            alert('Thêm mới tin tức thành công!');
		            $state.go('news', {}, { reload: true });
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.news.Alias = commonSvc.getAlias(input);
		    }

		}
	])
.controller('newsEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productCategoriesSvc', 'commonSvc', 'newsCategoriesSvc', 'newsSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productCategoriesSvc, commonSvc, newsCategoriesSvc, newsSvc) {
		    $scope.title = 'Cập nhật tin tức';
		    $scope.submitted = false;
		    $scope.news = {
		        Status: true,
		        Title: '',
		        Alias: ''
		    }

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.news.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    var loadCategory = function () {
		        newsCategoriesSvc.getListNewsCategory("").then(function (response) {
		            $scope.newsCategories = response.data;
		            $scope.loading = false;
		        }, function (error) {
		            $scope.loading = false;
		            console.log(error);
		        });
		    }

		    var loadDetail = function () {
		        newsSvc.getNewsById(parseInt($stateParams.id)).then(function (response) {
		            $scope.news = response.data;
		        }, function (err) {
		            console.log(err);
                });
		    }

		    var init = function () {
		        loadCategory();
		        loadDetail();
		    }

		    init();
		    

		    $scope.updateNews = function () {
		        $scope.submitted = true;
		        if ($scope.news.Title === '' || $scope.news.Title === null || $scope.news.Title.length > 256) {
		            return;
		        }

		        if ($scope.news.Alias === '' || $scope.news.Alias === null || $scope.news.Alias.length > 256) {
		            return;
		        }

		        if ($scope.news.CategoryId === null || $scope.news.CategoryId == undefined) {
		            return;
		        }

		        newsSvc.updateNews($scope.news).then(function (record) {
		            alert('Cập nhật tin tức thành công!');
		            $state.go('news', {}, { reload: true });
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.news.Alias = commonSvc.getAlias(input);
		    }

		}
	])