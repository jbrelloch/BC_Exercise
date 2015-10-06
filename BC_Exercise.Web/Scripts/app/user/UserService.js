'use strict';

angular.module('bcExerciseApp.services')
    .service('UserService', ['$http', function($http) {
        var service = {};

        service.getUser = function (id) {
            return $http.get('/api/user/getuser/' + id);
        };

        service.getAllUsers = function () {
            return $http.get('/api/user/getallusers');
        };

        service.addUser = function (model) {
            return $http.post('/api/user/adduser', model);
        };

        service.updateUser = function (model) {
            return $http.post('/api/user/updateuser', model);
        };

        service.deleteUser = function (id) {
            return $http.delete('/api/user/deleteuser/' + id);
        };

        return service;
    }]);