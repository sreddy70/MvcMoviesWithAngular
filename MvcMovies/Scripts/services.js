angular.module('Movies.Net.Service', [])
    .factory('Movies', ['$http', '$q', '$interpolate',

        function ($http, $q, $interpolate) {
            console.log('Movies factory()....');

            var q;

            var resolveUrl = $interpolate("{{baseUrl}}/{{object}}/{{action}}/{{id}}");
            var base_url = 'http://localhost:54283/movieapi';
            var object_Movies = 'Movies';

            var action_All = 'All';
            var action_ById = 'ById';
            var action_Create = 'CreateNew';
            var action_Update = 'Update';
            var action_Delete = 'Delete';

            var context = {
                baseUrl: base_url,
                object: object_Movies,
                action: undefined,
                id: undefined
            };

            return ({
                getAll: function() {
                    console.log('Movies.getAll()....');

                    if (q != null) {
                        q.reject();
                    }
                    q = $q.defer();

                    $http.defaults.headers.common.Accept = 'application/json, text/plain';
                    var req = {
                        method: "get"
                    }
                    var url = angular.copy(context);
                    url.action = action_All;
                    req.url = resolveUrl(url);
                    console.log(req.url);

                    $http(req).then(getAllMoviesSuccess, getAllMoviesError);
                    console.log('return q.promise');
                    return q.promise;
                },
                getById: function (id) {
                    console.log('Movies.getById()....' + id);

                    q.reject(); // Clear the promise cache
                    q = $q.defer();

                    $http.defaults.headers.common.Accept = 'application/json, text/plain';
                    var req = {
                        method: "get"
                    }
                    var url = angular.copy(context);
                    url.action = action_ById;
                    url.id = id;
                    req.url = resolveUrl(url);
                    console.log(req.url);

                    $http(req).then(movieSuccess, movieError);
                    console.log('return d.promise');
                    return q.promise;
                },
                save: function (movie) {
                    console.log('Movies.update()....');

                    q.reject(); // Clear the promise cache
                    q = $q.defer();

                    $http.defaults.headers.common.Accept = 'application/json, text/plain';
                    var req = {
                        method: "post"
                    }

                    req.data = angular.toJson(movie);

                    var url = angular.copy(context);
                    url.action = movie.id > 0 ?action_Update :action_Create;
                    req.url = resolveUrl(url);
                    console.log(req.url);
                    console.log(req.data);

                    $http(req).then(movieSuccess, movieError);
                    console.log('return d.promise');
                    return q.promise;
                },
                delete: function (id) {
                    console.log('Movies.delete()....');

                    q.reject(); // Clear the promise cache
                    q = $q.defer();

                    $http.defaults.headers.common.Accept = 'application/json, text/plain';
                    var req = {
                        method: "delete"
                    }

                    req.data = angular.toJson(id);

                    var url = angular.copy(context);
                    url.action = action_Delete;
                    url.id = id;
                    req.url = resolveUrl(url);
                    console.log(req.url);
                    console.log(req.data);

                    $http(req).then(movieSuccess, movieError);
                    console.log('return d.promise');
                    return q.promise;
                }
            });

            function getAllMoviesSuccess(resp) {
                console.log('$http.getAllMoviesSuccess()....');
                logToConsole(resp);
                
                if (resp.status === 200) {
                    q.resolve(resp.data);
                    //console.log('$http.getAllMoviesSuccess()...resolved');
                } else {
                    q.reject("An unknown error occurred.");
                    //console.log('$http.getAllMoviesSuccess()...rejected');
                }
            };

            function getAllMoviesError(resp) {
                console.log('$http.getAllMoviesError()....');
                logToConsole(resp);

                if (!angular.isObject(resp.data) ||
                    !resp.data.message) {
                    q.reject("An unknown error occurred.");
                }
                // Otherwise, use expected error message.
                q.reject(resp.data.message);
            };

            function movieSuccess(response) {
                console.log('movieSuccess()....');

                if (response.status === 200) {
                    console.log('response.data()...' + response.data);
                    q.resolve(response.data);
                    console.log('$http.movieSuccess()...resolved');
                } else {
                    q.reject("An unknown error occurred.");
                    console.log('$http.movieSuccess()...rejected');
                }
            };
            function movieError(response) {
                console.log('movieError()....');

                if (!angular.isObject(response.data) ||
                    !response.data.message) {
                    q.reject("An unknown error occurred.");
                }
                // Otherwise, use expected error message.
                q.reject(response.data.message);
            };

            function logToConsole(resp) {
                /*console.log('Data-' + resp.data);
                console.log('Status-' + resp.number);
                console.log('Config-' + resp.config.toString());
                console.log('statusText-' + resp.statusText);
                */
            };
        }
    ])

    .factory('SharedDataFactory',
        function () {
            console.log('SharedDataFactory.....')

            var movieId = 0;
            var addMode = editMode = false;
            var refreshList = true;
            var stateData = null;
            var map = {};

            return ({
                setMovieId: function(id) {
                    movieId = id;
                },
                getMovieId: function () {
                    return movieId;
                },

                setEditMode: function () {
                    editMode = true;
                    addMode = false;
                },
                isEditMode: function () {
                    return editMode;
                },

                setAddMode: function () {
                    editMode = false;
                    addMode = true;
                },
                isAddMode: function () {
                    return addMode;
                },

                setRefreshListMode: function (value) {
                    refreshList = value;
                    addMode = editMode = !value;
                },
                isRefreshListMode: function () {
                    return refreshList;
                },

                setData: function (data) {
                    stateData = data;
                },
                getData: function () {
                    return stateData;
                },

                set: function(key, value) {
                    map[key] = value;
                },
                get: function (key) {
                    return map[key];
                }
            });
        }
    );