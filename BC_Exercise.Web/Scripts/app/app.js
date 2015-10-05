'use strict';

var bcApp = angular.module('bcExerciseApp', [
    'ngRoute', 'bcExerciseApp.controllers',// 'bcExerciseApp.services', 'bcExerciseApp.directives'
]);

angular.module("bcExerciseApp.controllers", []);

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
        });
}
configFunction.$inject = ['$routeProvider', '$locationProvider'];

bcApp.config(configFunction);