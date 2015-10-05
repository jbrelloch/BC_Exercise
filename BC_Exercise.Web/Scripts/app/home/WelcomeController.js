angular.module('bcExerciseApp.controllers').
    controller('WelcomeController', ['$scope', function ($scope) {
        $scope.models = {
            helloAngular: 'I work!'
        };
    }]);