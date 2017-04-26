
angular.module('MvcMovies', [
    'ui.bootstrap',
    'ngAnimate',
    'ui.router',
    'bsLoadingOverlay',
    'bsLoadingOverlaySpinJs',
    'MvcMovies.Navigation',
    'MvcMovies.Controllers',
    'Movies.Net.Service'])

    .config(['$urlRouterProvider', '$stateProvider',
        function ($urlRouterProvider, $stateProvider) {

            //$urlRouterProvider.otherwise('/');

            $stateProvider
            .state('movieList', {
                url: '/movies',
                templateUrl: 'Templates/Movies/list.html',
                controller: 'MovieListCtrl'
            })
            .state('movieDetails', {
                url: '/movies/details',
                templateUrl: 'Templates/Movies/details.html',
                controller: 'MovieDetailsCtrl'
            })
        }])
    .constant('PageMode', {
        Read: 0,
        Add: 1,
        Edit: 2,
        Delete: 3,
        Refresh: 4
    })
    .run(['bsLoadingOverlayService',
        function (bsLoadingOverlayService) {
    	    bsLoadingOverlayService.setGlobalConfig({
    		    templateUrl: 'bsLoadingOverlaySpinJs'
    	    });
        }]);
    