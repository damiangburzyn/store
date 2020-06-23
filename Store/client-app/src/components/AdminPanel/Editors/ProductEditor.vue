<template>
    <v-card>
        <v-card-title class="headline grey lighten-2"
                      primary-title>
            {{ currentTitle }}
        </v-card-title>

        <v-card-text>
            <v-text-field label="Nazwa"
                          v-model="item.name"></v-text-field>

            <v-switch v-model="item.isBestseller" :label="`Bestseller`"></v-switch>



            <v-text-field label="Cena"
                          type="'number'"
                          v-model="item.currentPrice"></v-text-field>

            <v-text-field label="Obnizka z"
                          v-model="item.previousPrice"></v-text-field>

           
            <v-textarea name="input-7-1"
                        filled
                        v-model="item.description"
                        label="Opis"
                        auto-grow
                        value="The Woodman set to work at once, and so sharp was his axe that the tree was soon chopped nearly through."></v-textarea>

            <v-text-field label="Ilość sztuk"
                          type="'number'"
                          v-model="item.count"></v-text-field>

            <v-file-input :rules="imageRules"
                          accept="image/png, image/jpeg, image/bmp"
                          :placeholder="imagePlaceHolder()"
                          @change="onFilePicked"
                          multiple
                          prepend-icon="mdi-camera"
                          label="Obrazy">
            </v-file-input>





            <v-row>
                <v-col v-for="image in item.images"
                       :key="image.url"
                       class="d-flex child-flex"
                       cols="3">
                    <v-card flat tile class="d-flex">
                        <v-img :src="image.url"
                               :lazy-src="image.url"
                               aspect-ratio="1"
                               class="grey lighten-2">
                            <template v-slot:placeholder>
                                <v-row class="fill-height ma-0"
                                       align="center"
                                       justify="center">
                                    <v-progress-circular indeterminate color="grey lighten-5"></v-progress-circular>
                                </v-row>
                            </template>

                            <template v-slot:default>
                                <v-row class=" d-flex flex-row-reverse fill-height ma-0">

                                    <v-btn v-on:click="deleteImage(image)" class="mr-1" icon color="grey">
                                        <v-icon>mdi-close</v-icon>
                                    </v-btn>
                                </v-row>
                            </template>
                        </v-img>
                    </v-card>
                </v-col>
            </v-row>


        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
            <v-btn color="primary"
                   text
                   @click="closeDialog()">
                Anuluj
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary"
                   text
                   @click="save()">
                Zapisz
            </v-btn>
        </v-card-actions>
    </v-card>
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { SelectItem, Content } from '@/store/models';
    import { Product } from '@/store/models';
    import { productService } from "@/store/api";
    @Component
    export default class ProductEditor extends Vue {
        private msg!: string;
        @Prop(Number) readonly productId!: number;
        @Prop(Boolean) readonly IsShow!: boolean;
        public imageRules: [Function] = [
            (value: File[]) => !value || value.every(file => {
          return file.size < 2000000
            }) || 'Rozmiar obrazu powinien być poniżej 2 MB!',
        ];

        // public ShowParentCategoryDialog = false;
        private item: Product = this.getEmptyProduct();
        // public Show = this.IsShow;

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


         async  save() {

             try {
                 if (this.item != null && this.item?.id === 0) {
                     await productService.create(this.item);
                 }
                 else {
                     await productService.update(this.item);
                 }
                 this.$emit('saved-item');
             }
             catch (ex) {
                 console.error(ex);
             }
         }

        async LoadItem() {

            if (this.productId == 0) {
                this.item = this.getEmptyProduct();

            }
            else {

                var data = await productService.Get(this.productId);
                this.item = data;
            }
        }

        imagePlaceHolder() {
            return 'Kliknij aby wybrać obrazy';
        }


        getEmptyImage(): Content {
            const img: Content = {
                name: '',
                url: '',
                data: '',
            };
            return img;
        }

        getEmptyProduct(): Product {
            const prod: Product = {
                id: 0,
                name: '',
                isBestseller: false,
                currentPrice: 0,
                previousPrice: 0,
                description: '',
                images: [],
                movies: [],
                count: 0
            };
            return prod
        }

        onFilePicked(files: File[]) {
            let self = this;

            files.forEach(file => {
                let image = self.getEmptyImage();

                if (file !== undefined) {
                    image.name = file.name
                    if (image.name.lastIndexOf('.') <= 0) {
                        return
                    }
                    const fr = new FileReader()
                    fr.readAsDataURL(file)
                    fr.addEventListener('load', (e) => {
                        image.data = fr.result as string || '';
                        image.url = URL.createObjectURL(file);  // this is an image file that can be sent to server...
                        self.item.images.push(image);
                    })
                }
            })
        }

        deleteImage(image: Content) {
            const index = this.item.images.indexOf(image);
            if (index > -1) {
                this.item.images.splice(index, 1);
            }
        }



        closeDialog() {
            this.$emit("dialog-closed");
        }

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

        get currentTitle() {
            if (this.item.id === 0) {
                return "Utwórz nowy"
            }
            else
                return "Edytuj"
        }
    }
</script>