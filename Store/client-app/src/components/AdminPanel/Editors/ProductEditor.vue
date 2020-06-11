<template>



    <div class="text-center">
    Edytor
    </div>   
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { SelectItem, Content } from '@/store/models';
    import { Product} from '@/store/models';
    import { ProductService } from "@/store/api";
    @Component
    export default class ProductEditor extends Vue {
        private msg!: string;
        @Prop(Number) readonly ProductId!: number;
        @Prop(Boolean) readonly IsShow!: boolean;
       
       // public ShowParentCategoryDialog = false;
        private Item: Product = this.getEmptyProduct();
       // public Show = this.IsShow;
       // public LocalCatId = this.CategoryId;
       // public ParentCategoryName: string = '';
       // public ImageRules: [Function] = [
       //     (value: File) => !value || value.size < 2000000 || 'Rozmiar obrazu powinien być poniżej 2 MB!',
       // ];
       // //public SelectCategories: SelectItem<number>[] = [
       // //    { Text: "Category1", Value: 1 },
       // //    { Text: "Category2", Value: 2 },
       // //    { Text: "Category3", Value: 3 }
       // //];

       // SelectedParentCategory: SelectItem<number | null> = { Text: "----------", Value: null }


       // async  Save() {

       //     try {
       //         if (this.Item != null && this.Item?.id === 0) {
       //             await CategoryService.create(this.Item);
       //         }
       //         else {
       //             await CategoryService.update(this.Item);
       //         }
       //         this.$emit('saved-item');
       //     }
       //     catch (ex) {
       //         console.error(ex);
       //     }
       // }

       //async LoadItem() {

       //     if (this.CategoryId == 0) {
       //         this.Item = this.getEmptyCategory();
       //         this.ParentCategoryName = '';               
       //     }
       //     else {

       //         var data = await CategoryService.Get(this.CategoryId);
       //         if (data.logo === null) {
       //             data.logo = this.getEmptyLogo();
       //         }

       //         this.Item = data;
       //     }
       // }

       // imagePlaceHolder() {
       //     if (this.Item.logo.name !== '') {
       //         return this.Item.logo.name;

       //     }
       //     else return 'Wybierz obraz';
       // }

       //getEmptyLogo() :Image {
       //    const img: Image = {
       //        name: '',
       //        url: '',
       //        data: '',
       //    };
       //      return img;
       // }

        getEmptyProduct(): Product {
            const prod: Product = {
                id: 0,
                name: '',             
                isBestseller: false,               
                currentPrice: 0,
                previousPrice: 0,
                description: '',             
                images: [],
                movies:[],
                count: 0   
            };
            return prod
        }

       // onFilePicked(file: File) {
       //     let self = this;
       //     self.Item.logo = self.getEmptyLogo();

       //     if (file !== undefined) {
       //         self.Item.logo.name = file.name
       //         if (this.Item.logo.name.lastIndexOf('.') <= 0) {
       //             return
       //         }
       //         const fr = new FileReader()
       //         fr.readAsDataURL(file)
       //         fr.addEventListener('load', (e) => {
       //             self.Item.logo.data = fr.result as string || '';
       //             self.Item.logo.url = URL.createObjectURL(file);  // this is an image file that can be sent to server...
       //             console.log(e);
       //         })
       //     } else {
       //         this.Item.logo.name = '';
       //         this.Item.logo.data = '';
       //         this.Item.logo.url = '';
       //     }
       // }

       // setParentCategory(item: Category) {
       //     this.ParentCategoryName = item.name;
       //     this.Item.parentCategoryId = item.id;
       //     this.ShowParentCategoryDialog = false;
       // }

       // onParentCategoryDelete() {
       //     this.ParentCategoryName = '';
       //     this.Item.parentCategoryId =null;
       // }

       // // dialog ma możliwość lokalnej zmiany property
       // @Watch('Show')
       // onPropertyShowChanged(value: boolean, oldValue: boolean) {
       //     if (value == false) {
       //         this.closeDialog();
       //     }       
       // }
       
       // closeDialog() {
       //     this.$emit("dialog-closed");
       // }

       // ///gdy zmienia się property przez parenta
       // @Watch('IsShow')
       // onPropertyIsShowChanged(value: boolean, oldValue: boolean) {
       //     this.Show = value;
           
       // }

       // @Watch('CategoryId')
       // onPropertyCategoryIdChanged(value: number, oldValue: number) {
       //     this.LocalCatId = value;
       //     this.LoadItem(); 
       // }

       // get currentTitle() {
       //     if (this.Item.id === 0) {
       //         return "Utwórz nowy"
       //     }
       //     else
       //         return "Edytuj"
       // }
    }
</script>