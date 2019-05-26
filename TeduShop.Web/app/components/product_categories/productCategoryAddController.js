(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            Status: true
        }

        $scope.AddProductCategory = AddProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function AddProductCategory() {
            apiService.post('api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess('Bản ghi "' + result.data.Name + '" đã được tạo.');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayWarning('Tạo mới không thành công. Vui lòng kiểm tra lại.');
                });
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Lỗi: Không thể tải được dữ liệu dropdownlist.');
            });
        }

        loadParentCategory();
    }

})(angular.module('tedushop.product_categories'));