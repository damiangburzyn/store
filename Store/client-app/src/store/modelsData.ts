import { Content } from '@/store/models';

export class Category {
    id: number;
    sortOrder: number | undefined;
    name: string;
    shortName: string;
    logo: Content;
    parentCategoryId: number | null | undefined;
    subCategories: Array<Category | undefined>

    constructor() {
        this.id = 0;
        this.sortOrder = 0;
        this.name = '';
        this.shortName = "";
        this.parentCategoryId = null;
        this.logo = { data: "", name: "", url: "" };
        this.subCategories = [];
    }
}



export class DataTableSearchViewModel<T>{
    total: number;
    perPage: number;
    currentPage: number;
    lastPage: number;
    from: number;
    to: number;
    nextPageUrl: string;
    prevPageUrl: string;
    data: Array<T>;

    constructor() {
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

}
