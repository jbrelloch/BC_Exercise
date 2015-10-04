/*'use strict';

angular.module('bcExerciseApp', [
    'ngRoute', 'bcExerciseApp.controllers',// 'bcExerciseApp.services', 'bcExerciseApp.directives'
]).config([
    /*'$routeProvider',
    function($routeProvider) {
        //setup routes
        //$routeProvider.when('/', { templateUrl: '/', reloadOnSearch: false });

        //$routeProvider.when('/UserPage/User', { templateUrl: '/UserPage/User/Index', reloadOnSearch: false });
    }#1#
]).run([]);

angular.module("bcExerciseApp.controllers", []);*/

var HomeController = function ($scope) {
    $scope.models = {
        helloAngular: 'I work!'
    };
}

HomeController.$inject = ['$scope'];

var bcExerciseApp = angular.module('bcExerciseApp', []);

bcExerciseApp.controller('HomeController', HomeController);