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