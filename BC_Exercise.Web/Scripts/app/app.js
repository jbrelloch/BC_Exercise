'use strict';

var bcApp = angular.module('bcExerciseApp', [
    'ngRoute', 'bcExerciseApp.controllers', 'bcExerciseApp.services'//, 'bcExerciseApp.directives'
]);

angular.module("bcExerciseApp.controllers", []);
angular.module("bcExerciseApp.services", []);

var configFunction = function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', {
            templateUrl: 'home/welcome'
        })
        .when('/home', {
            templateUrl: 'home/welcome'
        })
        .when('/home/index', {
            templateUrl: 'home/welcome'
        })
        .when('/user', {
            templateUrl: 'user/users'
        })
        .when('/user/add', {
            templateUrl: 'user/add'
        })
        .when('/user/edit/:id', {
            templateUrl: function (params) { return '/user/edit?id=' + params.id; }
        });
    
}
configFunction.$inject = ['$routeProvider', '$locationProvider'];

bcApp.config(configFunction);