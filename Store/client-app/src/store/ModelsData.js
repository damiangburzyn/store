var Category = /** @class */ (function () {
    function Category() {
        this.id = 0;
        this.sortOrder = 0;
        this.name = '';
        this.shortName = "";
        this.parentCategoryId = null;
        this.logo = { data: "", name: "", url: "" };
        this.subCategories = [];
    }
    return Category;
}());
export { Category };
var DeliveryMethod = /** @class */ (function () {
    function DeliveryMethod() {
        this.id = 0;
        this.name = '';
        this.description = '';
    }
    return DeliveryMethod;
}());
export { DeliveryMethod };
var ProductCategory = /** @class */ (function () {
    function ProductCategory() {
        this.id = 0;
        this.productId = 0;
        this.categoryId = 0;
        this.category = null;
        this.product = null;
    }
    return ProductCategory;
}());
export { ProductCategory };
var Product = /** @class */ (function () {
    function Product() {
        this.images = [];
        this.movies = [];
        this.deliveryMethods = [];
        this.productCategories = [];
        this.id = 0;
        this.isBestseller = false;
        this.name = '';
        this.currentPrice = 0.00;
        this.previousPrice = 0.00;
        this.description = '';
        this.count = 0;
    }
    return Product;
}());
export { Product };
var ProductDeliveryMethod = /** @class */ (function () {
    function ProductDeliveryMethod() {
        this.id = 0;
        this.deliveryId = 0;
        this.productId = 0;
        this.maxCountInPackage = 1;
        this.price = 0.00;
        this.delivery = new DeliveryMethod();
    }
    return ProductDeliveryMethod;
}());
export { ProductDeliveryMethod };
var DataTableSearchViewModel = /** @class */ (function () {
    function DataTableSearchViewModel() {
        this.total = 0;
        this.perPage = 0;
        this.currentPage = 0;
        this.lastPage = 0;
        this.from = 0;
        this.to = 0;
        this.nextPageUrl = '';
        this.prevPageUrl = '';
        this.data = [];
    }
    return DataTableSearchViewModel;
}());
export { DataTableSearchViewModel };
//# sourceMappingURL=ModelsData.js.map