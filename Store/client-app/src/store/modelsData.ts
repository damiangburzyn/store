import { Content} from '@/store/models';

export class Category {
    id: number;
    sortOrder: number | undefined;
    name: string;
    shortName: string;
    logo: Content;
    parentCategoryId: number | null|undefined;
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


