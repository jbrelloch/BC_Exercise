angular.module('bcExerciseApp.controllers').
    controller('AddUserController', ['$scope', '$location', 'UserService', function ($scope, $location, UserService) {

        $scope.user = {
            FirstName: "",
            LastName: "",
            Email: "",
            StreetAddress: "",
            City: "",
            State: "",
            Zip: ""
        };

        $scope.errors = {
            FirstName: "",
            LastName: "",
            Email: "",
            StreetAddress: "",
            City: "",
            State: "",
            Zip: ""
        };

        $scope.addUser = function () {
            if (validator()) {
                return;
            }

            UserService.addUser($scope.user).success(function () {
                $location.path('/user');
            });
        };

        var validator = function () {
            var error = false;
            if ($scope.user.FirstName === null || $scope.user.FirstName === "") {
                $scope.errors.FirstName = "Please enter a first name.";
                error = true;
            }
            if ($scope.user.LastName === null || $scope.user.LastName === "") {
                $scope.errors.FirstName = "Please enter a last name.";
                error = true;
            }
            if ($scope.user.Email === null || $scope.user.Email === "") {
                $scope.errors.FirstName = "Please enter a email.";
                error = true;
            }
            if ($scope.user.StreetAddress === null || $scope.user.StreetAddress === "") {
                $scope.errors.StreetAddress = "Please enter a street address.";
                error = true;
            }
            if ($scope.user.City === null || $scope.user.City === "") {
                $scope.errors.City = "Please enter a city.";
                error = true;
            }
            if ($scope.user.State === null || $scope.user.State === "") {
                $scope.errors.State = "Please enter a state.";
                error = true;
            }
            if ($scope.user.Zip === null || $scope.user.Zip === "" || isNaN(parseFloat($scope.user.Zip)) || !isFinite($scope.user.Zip)) {
                $scope.errors.Zip = "Please enter a zip code.";
                error = true;
            }
            return error;
        }
    }]);