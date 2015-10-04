'use strict';

var bcApp = angular.module('bcExerciseApp', [
    'ngRoute', 'bcExerciseApp.controllers',// 'bcExerciseApp.services', 'bcExerciseApp.directives'
]);

angular.module("bcExerciseApp.controllers", []);

var configFunction = function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider.when('/', { templateUrl: '/', reloadOnSearch: false });
    $routeProvider.when('/User', { templateUrl: '/User/Index', reloadOnSearch: false });
    $routeProvider.when('/UserPage', { templateUrl: '/UserPage/User/Index.cshtml', reloadOnSearch: false });
    $routeProvider.when('/UserPage/User', { templateUrl: 'UserPage/User/Index.cshtml', reloadOnSearch: false });
}
configFunction.$inject = ['$routeProvider', '$locationProvider'];

bcApp.config(configFunction);