import { Image} from '@/store/models';

export class Category {
    Id: number;
    SortOrder: number | undefined;
    Name: string;
    ShortName: string;
    Logo: Image;
    ParentCategoryId: number | undefined;
    SubCategories: Array<Category | undefined>

    constructor() {
        this.Id = 0;
        this.SortOrder = 0;
        this.Name = '';
        this.ShortName = "";
        this.ParentCategoryId = 0;
        this.Logo = { Data: "", Name: "", Url: "" };
        this.SubCategories = [];
      
    }
}


