(function (app) {
    'use strict';

    app.controller('applicationUserAddController', applicationUserAddController);

    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];

    function applicationUserAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.account = {
            Groups: [],
            BirthDay: new Date()
        }

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2030, 12, 31),
            minDate: new Date(1920, 1, 1),
            startingDay: 1,
            showWeeks: true
        };

        $scope.popup = {
            opened: false
        };

        $scope.open = function () {
            $scope.popup.opened = true;
        };

        $scope.format = 'dd/MM/yyyy';

        $scope.addAccount = addAccount;

        function addAccount() {
            apiService.post('/api/applicationUser/add', $scope.account, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess('Tài khoản ' + $scope.account.Name + ' đã được thêm mới.');
            $location.url('application_users');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });
        }

        loadGroups();

    }
})(angular.module('tedushop.application_users'));