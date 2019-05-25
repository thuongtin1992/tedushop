(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function productCategoryAddController(apiService, $scope, notificationService, $state) {
        $scope.productCategory = {
            Status: true,
            Name: "Danh mục 1",
            Alias: "danh-muc-1",
            MetaKeyword: "danh mục 1"
        }

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            apiService.post('api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess('Bản ghi ' + result.data.Name + ' đã được tạo.');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayWarning('Tạo mới không thành công.');
                });
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                notificationService.displayError('Lỗi: Không thể tải được dữ liệu.');
            });
        }

        loadParentCategory();
    }

})(angular.module('tedushop.product_categories'));