angular.module('MvcMovies.Controllers', [])

.controller('MovieListCtrl', ['$scope', '$state', 'Movies', 'PageMode', 'SharedDataFactory', 'bsLoadingOverlayService',
    function ($scope, $state, Movies, PageMode, SharedDataFactory, bsLoadingOverlayService) {
        console.log('In MovieListCtrl...');

        $scope._arrSelectedRowsIdx = [];
        $scope._selectedRowCount = 0;

        //Initializations
        $scope.movies = null;
        if (SharedDataFactory.get('PageMode') == null) {
            SharedDataFactory.set('PageMode', PageMode.Refresh);
        }

        $scope.List = function () {
            console.log('In List.()...');

            console.log('PageMode....' + SharedDataFactory.get('PageMode'));
            if (PageMode.Refresh == SharedDataFactory.get('PageMode')) {
                // Comes here when the 
                //      a. list is fetched or 
                //      b. when the movie object is saved and the user clicks 'Back' 
                console.log('...Refresh');
                bsLoadingOverlayService.start();
                $scope.btnDisableDeleteMovies = $scope.btnDisableEditMovies = true;
                $scope.btnDisableAddMovies = false;

                //console.log('_selectedRowCount....' + SharedDataFactory.get('$scope._selectedRowCount'));
                //if (SharedDataFactory.get('$scope._selectedRowCount') > 0) {
                //    console.log('>0');
                //    $scope._arrSelectedRowsIdx = SharedDataFactory.get('$scope._arrSelectedRowsIdx');
                //    $scope._selectedRowCount = SharedDataFactory.get('$scope._selectedRowCount');
                //    $scope.btnDisableEditMovies = true;
                //}
                //else {
                //    $scope._arrSelectedRowsIdx = [];
                //    $scope._selectedRowCount = 0;
                //}

                // Get listing from the service
                Movies.getAll()
                .then(
                    function (data) {
                        //console.log('Movies.List()...successful.');
                        $scope.movies = data;
                        bsLoadingOverlayService.stop();
                    },
                    function (err) {
                        console.log('Movies.List()...error.' + err);
                        bsLoadingOverlayService.stop();
                    });
            }
            else {
                SharedDataFactory.set('PageMode', PageMode.Refresh);
                $scope.movies = SharedDataFactory.get('$scope.movies');
                $scope._arrSelectedRowsIdx = SharedDataFactory.get('$scope._arrSelectedRowsIdx');
                $scope._selectedRowCount = SharedDataFactory.get('$scope._selectedRowCount');
                $scope.btnDisableEditMovies = !($scope._selectedRowCount == 1);
                $scope.btnDisableAddMovies = false;
            }
            
        };

        $scope.ToggleSelectAll = function () {
            console.log('In ToggleSelectAll()...');

            $scope.selectAll = !$scope.selectAll;
            angular.forEach($scope.movies, function (x) {
                x.checked = $scope.selectAll;
                if ($scope.selectAll) {
                    if (-1 >= $scope._arrSelectedRowsIdx.indexOf(x.id)) {
                        $scope._selectedRowCount++;
                        $scope._arrSelectedRowsIdx.push(x.id);
                    }
                }
                else {
                    $scope._selectedRowCount--;
                    var idx = $scope._arrSelectedRowsIdx.indexOf(x.id);
                    if (idx > -1) $scope._arrSelectedRowsIdx.splice(idx, 1);
                }
            });
        };

        $scope.ToggleSelectRow = function (movie) {
            console.log('In ToggleRowSelect()...');

            $scope.selectAll = false;

            movie.checked = !movie.checked;
            if (movie.checked) {
                $scope._selectedRowCount++;
                $scope._arrSelectedRowsIdx.push(movie.id);
            } else {
                $scope._selectedRowCount--;
                var idx = $scope._arrSelectedRowsIdx.indexOf(movie.id);
                if (idx > -1) $scope._arrSelectedRowsIdx.splice(idx, 1);
            }

            // Enable edit button only if 1 row is selected
            $scope.btnDisableDeleteMovies = $scope.btnDisableEditMovies = !($scope._selectedRowCount == 1);
        };

        $scope.EditRow = function () {
            console.log('In EditRow...');

            if ($scope._selectedRowCount == 1) {
                SharedDataFactory.set('PageMode', PageMode.Edit);
                SharedDataFactory.set('$scope._selectedRowCount', $scope._selectedRowCount);
                SharedDataFactory.set('$scope._arrSelectedRowsIdx', $scope._arrSelectedRowsIdx);
                SharedDataFactory.set('$scope.movies', $scope.movies);
                $state.go('movieDetails');
            }
        };

        $scope.AddRow = function () {
            console.log('In AddMovie()...');

            SharedDataFactory.set('PageMode', PageMode.Add);
            $state.go('movieDetails');
        };

        $scope.Delete = function () {
            console.log('In DeleteMovie()...');

            bsLoadingOverlayService.start();
            if ($scope._selectedRowCount == 1 &&
                $scope._arrSelectedRowsIdx.length == 1) {
                Movies.delete($scope._arrSelectedRowsIdx[0])
                .then(
                    function (data) {
                        console.log('Movies.delete()...successful.');
                        $scope.List();
                        bsLoadingOverlayService.stop();
                    },
                    function (err) {
                        console.log('Movies.delete()...error.' + err);
                        bsLoadingOverlayService.stop();
                });
            }
        };

        console.log('In2 MovieListCtrl...');
    }])

    .controller('MovieDetailsCtrl', ['$scope', '$state', 'Movies', 'PageMode', 'SharedDataFactory', 'bsLoadingOverlayService',
        function ($scope, $state, Movies, PageMode, SharedDataFactory, bsLoadingOverlayService) {
            console.log('In MovieDetailsCtrl...');

            $scope.getDetails = function () {
                console.log('In Details.()...');

                $scope.movie = null;
                
                if (PageMode.Edit == SharedDataFactory.get('PageMode')) {
                    bsLoadingOverlayService.start();

                    var id = SharedDataFactory.get('$scope._arrSelectedRowsIdx'); //array
                    if (angular.isArray(id) &&
                        id.length == 1) {
                        id = id[0];
                    }
                    console.log('Id from List-' + id);

                    Movies.getById(id)
                    .then(
                        function (data) {
                            console.log('Movies.getById()...successful.');
                            data.releaseDate = new Date(data.releaseDate);
                            $scope.movie = data;
                            bsLoadingOverlayService.stop();
                        },
                        function (err) {
                            console.log('Movies.getById()...error.' + err);
                            bsLoadingOverlayService.stop();
                        });
                } //Edit mode

            };

            $scope.Save = function () {
                console.log('In Save()...');

                bsLoadingOverlayService.start();
                //if (PageMode.Edit == SharedDataFactory.get('PageMode')) {
                    Movies.save($scope.movie)
                    .then(
                    function (data) {
                        console.log('Movies.Save()...successful.');
                        SharedDataFactory.set('PageMode', PageMode.Refresh);
                        bsLoadingOverlayService.stop();
                    },
                    function (err) {
                        console.log('Movies.Save()...error.' + err);
                        bsLoadingOverlayService.stop();
                    });
                //} // Edit mode
            };

            $scope.Back = function () {
                console.log('In Back...');
                console.log('_selectedRowCount...' + SharedDataFactory.get('$scope._selectedRowCount'));

                $state.go('movieList');
            };

            console.log('In2 MovieDetailsCtrl...');
        }]);
