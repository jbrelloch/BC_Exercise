angular.module('bcExerciseApp.controllers').
    controller('EditUserController', ['$scope', '$location', '$routeParams', 'UserService', function ($scope, $location, $routeParams, UserService) {

        $scope.isEditingUser = false;

        $scope.user = {
            Id: "",
            FirstName: "",
            LastName: "",
            FullName: "",
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

        $scope.userCopy = {};

        $scope.beginEdit = function () {
            $scope.isEditingUser = true;

            $scope.userCopy = angular.copy($scope.user);
        };

        $scope.saveUserEdits = function () {
            if (validator()) {
                return;
            }

            $scope.isEditingUser = false;

            UserService.updateUser($scope.user).success(function (result) {
                $scope.user = result;
                $scope.isEditingUser = false;
            });
        };

        $scope.cancelUserEdits = function () {
            $scope.isEditingUser = false;
            $scope.errors = {};

            $scope.user = $scope.userCopy;
        };

        $scope.deleteUser = function () {
            $('.modal-backdrop').remove();

            UserService.deleteUser($routeParams.id).success(function () {
                $location.path('/user');
            });
        };
        
        if ($routeParams.id != null) {
            UserService.getUser($routeParams.id).success(function (result) {
                $scope.user = result;
                $scope.isEditingUser = false;
            });
        }

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