import { Image} from '@/store/models';

export class Category {
    Id: number;
    SortOrder: number | undefined;
    Name: string;
    ShortName: string;
    Logo: Image;
    ParentCategoryId: number | null|undefined;
    SubCategories: Array<Category | undefined>

    constructor() {
        this.Id = 0;
        this.SortOrder = 0;
        this.Name = '';
        this.ShortName = "";
        this.ParentCategoryId = null;
        this.Logo = { Data: "", Name: "", Url: "" };
        this.SubCategories = [];
      
    }
}


