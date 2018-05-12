angular.module('electronicStoreApp.screens.home',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.directives',
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
		'$state', '$rootScope', '$scope', '$stateParams', '$q', 'apiSvc',
		function ($state, $rootScope, $scope, $stateParams, $q, apiSvc) {

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
		  
		   //var data = {
		   //    labels: ["January", "February", "March", "April", "May"],
		   //    datasets: [
           //        {
           //            label: "My First dataset",
           //            fillColor: "rgba(220,220,220,0.2)",
           //            strokeColor: "rgba(220,220,220,1)",
           //            pointColor: "rgba(220,220,220,1)",
           //            pointStrokeColor: "#fff",
           //            pointHighlightFill: "#fff",
           //            pointHighlightStroke: "rgba(220,220,220,1)",
           //            data: [65, 59, 80, 81, 56]
           //        },
           //        {
           //            label: "My Second dataset",
           //            fillColor: "rgba(151,187,205,0.2)",
           //            strokeColor: "rgba(151,187,205,1)",
           //            pointColor: "rgba(151,187,205,1)",
           //            pointStrokeColor: "#fff",
           //            pointHighlightFill: "#fff",
           //            pointHighlightStroke: "rgba(151,187,205,1)",
           //            data: [28, 48, 40, 19, 86]
           //        }
		   //    ]
		   //};
		   //var pdata = [
           //  {
           //      value: 300,
           //      color: "#46BFBD",
           //      highlight: "#5AD3D1",
           //      label: "Complete"
           //  },
           //  {
           //      value: 50,
           //      color: "#F7464A",
           //      highlight: "#FF5A5E",
           //      label: "In-Progress"
           //  }
		   //]

		   //var ctxl = $("#lineChartDemo").get(0).getContext("2d");
		   //var lineChart = new Chart(ctxl).Line(data);

		   //var ctxp = $("#pieChartDemo").get(0).getContext("2d");
		   //var pieChart = new Chart(ctxp).Pie(pdata);
        }
	]);