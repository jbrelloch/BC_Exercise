angular.module('bcExerciseApp.controllers').
    controller('UserController', ['$scope', '$location', 'UserService', function ($scope, $location, UserService) {
        $scope.models = {
            helloAngular: 'I work to!'
        };

        $scope.users = [];

        $scope.getAllUsers = function() {
            UserService.getAllUsers().success(function (result) {
                $scope.users = result;
            });
        };

        $scope.fullAddress = function (index) {
            if (!$scope.users[index]
                || !$scope.users[index].StreetAddress
                || !$scope.users[index].City
                || !$scope.users[index].State
                || !$scope.users[index].Zip)
                return "";

            return $scope.users[index].StreetAddress + ", " + $scope.users[index].City + ", " + $scope.users[index].State + " " + $scope.users[index].Zip;
        };

        $scope.addUserTest = function () {
            UserService.addUserTest().success(function () {
                $scope.getAllUsers();
            });
        };

        $scope.editUser = function (id) {
            var cutId = id.split("/")[1];
            $location.path('/user/edit/' + cutId);
        };

        $scope.getAllUsers();
    }]);