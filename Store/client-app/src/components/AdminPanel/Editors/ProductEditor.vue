<template>
    <div>
        <v-dialog v-model="show"
                  persistent
                  max-width="550px">
            <v-card>
                <v-card-title class="headline grey lighten-2"
                              primary-title>
                    {{ currentTitle }}
                </v-card-title>

                <v-card-text>
                    <v-text-field label="Nazwa" v-model="item.name"></v-text-field>

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

                    <!--<v-text-field label="Ilość sztuk"
                                  type="'number'"
                                  v-model="item.count"></v-text-field>-->

                    <v-file-input :rules="imageRules"
                                  accept="image/png, image/jpeg, image/bmp"
                                  :placeholder="imagePlaceHolder()"
                                  @click:clear="onImagesClear"
                                  hide-input
                                  update:error="onImagesupdateError"
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
                    <br/>

                    <v-row>

                        <v-card flat width="100%" @click="showProductCategoryDialog = true">
                            <label class="v-label v-label--active theme--light" style="left: 0px; right: auto; position: absolute;">Kategorie produktu</label>
                            <br />
                            <v-row>
                                <v-chip class="ma-2" v-for="(prodCat, index)  in productCategoryNames" :key="index">
                                    {{prodCat}}
                                </v-chip>
                            </v-row>
                            <v-divider></v-divider>
                        </v-card>
                    </v-row>

                </v-card-text>



                <!--<v-text-field label="Kategorie produktu" readonly
                              v-model="ProductCategoryNames"
                              @click="ShowParentCategoryDialog = true"
                              :clearable="true"
                              @click:clear="onParentCategoryDelete()">

                </v-text-field>-->

                <!--<v-card-title class=" grey lighten-2">
                    Opcje dostawy
                </v-card-title>
                <v-card max-height="400" class="overflow-y-auto">

                    <v-card-text>
                        <v-row v-for="(sdm, index) in selectDeliveryMethods" :key="index">
                            <v-col>
                                <v-switch v-model="sdm.isSelected"
                                          :label="sdm.item.delivery.name"></v-switch>
                            </v-col>
                            <v-col>
                                <v-text-field label="Cena"
                                              type="'number'"
                                              :disabled="!sdm.isSelected"
                                              v-model="sdm.item.price"></v-text-field>
                            </v-col>
                            <v-col>
                                <v-text-field label="Max w paczce"
                                              type="'number'"
                                              :disabled="!sdm.isSelected"
                                              v-model="sdm.item.maxCountInPackage"></v-text-field>
                            </v-col>
                        </v-row>
                    </v-card-text>
                </v-card>-->


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
        </v-dialog>


        <v-dialog scrollable v-model="showProductCategoryDialog" width="600">
            <v-card>
                <v-card-title class="headline grey lighten-2" primary-title> Wybierz kategorie </v-card-title>
                <v-card-text>
                    <v-treeview :items="treeSelectCategory"
                                item-children="children"
                                item-key="item.id"
                                item-text="item.name">

                        <template v-slot:prepend="{ item }">

                            <div style="padding-left:20px; margin-top:-12px">
                                <!--v-on:click="manageProductCategories(item.isSelected, item)"-->

                                <v-switch @click.native="manageProductCategories(item)" style="display:inline; margin-left:10px" x-small v-model="item.isSelected"></v-switch>
                            </div>
                            <!--<v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                                   title="Wybierz"
                                   elevation="0"
                                   v-on:click="setParentCategory(item)">
                            </v-btn>-->
                        </template>
                    </v-treeview>
                </v-card-text>
                <v-card-actions>                 
                    <v-spacer></v-spacer>
                    <v-btn color="primary"
                           text
                           @click="showProductCategoryDialog = false">
                        Zamknij
                    </v-btn>

                    <v-spacer></v-spacer>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { Content, SelectModel, TreeSelectModel } from '@/store/models';
    import { Product, ProductDeliveryMethod, Category, ProductCategory } from '@/store/modelsData';
    import { productService, deliveryMehodService, categoryService } from "@/store/api";
    @Component
    export default class ProductEditor extends Vue {
        private msg!: string;
        @Prop({ default: 0, validator: (x) => x >= 0, type: Number, required: true }) readonly productId!: number;
        @Prop( ) readonly isShow!: boolean;
        public imageRules: [Function] = [
            (value: File[]) => !value || value.every(file => {
                return file.size < 2000000
            }) || 'Rozmiar obrazu powinien być poniżej 2 MB!',
        ];

        private showProductCategoryDialog = false;
        private productCategoryNames: string[] = [];


        private item: Product = this.getEmptyProduct();
        public show = this.isShow;
      //  public selectDeliveryMethods = new Array<SelectModel<ProductDeliveryMethod>>();
        public treeSelectCategory = new Array<TreeSelectModel<Category>>();

        async created() {
            await this.loadTree();
        }

        // manageProductCategory(isSelected: boolean, treeSelect: TreeSelectModel<Category>) {
        manageProductCategories(treeSelect: TreeSelectModel<Category>) {
            let pc = this.item.productCategories.find(x => x.categoryId === treeSelect.item?.id);
            const pcName: string | undefined = this.productCategoryNames.find(n => n === treeSelect.item?.name);
            if (pc !== undefined && pc !== null) {
                this.item.productCategories.splice(this.item.productCategories.indexOf(pc), 1);
            }
            else {
                pc = new ProductCategory();
                pc.categoryId = treeSelect.item?.id ?? 0;
                pc.productId = this.item.id;
                this.item.productCategories.push(pc);
            }

            if (pcName !== undefined && pcName !== null) {
                this.productCategoryNames.splice(this.productCategoryNames.indexOf(pcName), 1);
            }
            else {
                const d = treeSelect?.item?.name ?? '';
                this.productCategoryNames.push(d);
            }


        }

        async save() {

            //const selectedDeliveryMethods = new Array<ProductDeliveryMethod>();
            //this.selectDeliveryMethods.forEach((select) => {
            //    if (select.isSelected && select.item != null) {
            //        selectedDeliveryMethods.push(select.item);
            //    }
            //})

            //this.item.deliveryMethods = selectedDeliveryMethods;

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

        async loadItem() {

            if (this.productId == 0) {
                this.item = this.getEmptyProduct();
                this.productCategoryNames = [];
              //  this.selectDeliveryMethods = await this.getDeliveryMethods();
                await this.loadTree();
            }
            else {

                await productService.get(this.productId).then((data) => {
                    this.item = data;
                });
    
                    await this.loadTree();
                    //const prodDelMethods = await this.getDeliveryMethods();
                    //if (this.productId !== 0) {

                    //    this.item.deliveryMethods.forEach(dm => {

                    //        prodDelMethods.forEach(pdm => {

                    //            if (pdm.item?.deliveryId === dm.deliveryId) {

                    //                pdm.isSelected = true;
                    //                pdm.item.id = dm.id;
                    //                pdm.item.maxCountInPackage = dm.maxCountInPackage;
                    //                pdm.item.price = dm.price;
                    //            }
                    //        })

                    //    })
                    //}

                    //this.selectDeliveryMethods = prodDelMethods;
                



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

            return new Product();
        }



        onImagesupdateError(e: Error) {
            console.log(e);
        }

        onImagesClear() {
            this.item.images = [];
        }

        onFilePicked(files: File[]) {
           

            files.forEach(file => {
                const image = this.getEmptyImage();

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


                        this.item.images.forEach(image => {

                            if (image.name === file.name) {
                                const index = this.item.images.indexOf(image);
                                if (index !== -1) this.item.images.splice(index, 1);
                            }

                        })
                        this.item.images.push(image);
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

        async getDeliveryMethods() {
            const selectDeliveryMethods = new Array<SelectModel<ProductDeliveryMethod>>();

           
            const methods = await deliveryMehodService.list();
            methods.forEach(method => {
                const prodDeliveryMethod: ProductDeliveryMethod = {
                    id: 0,
                    delivery: method,
                    deliveryId: method.id,
                    productId: this.productId,
                    maxCountInPackage: 0,
                    price: 0
                }

                const model: SelectModel<ProductDeliveryMethod> = {
                    isSelected: false,
                    item: prodDeliveryMethod,
                }
                selectDeliveryMethods.push(model);
            })

            return selectDeliveryMethods
        }


        async loadTree() {
            const categories = await categoryService.tree()
            this.treeSelectCategory = this.buildTreeSelectModel(categories);
        }

        buildTreeSelectModel(categories: Array<Category | undefined>): Array<TreeSelectModel<Category>> {

            const treeSelect: Array<TreeSelectModel<Category>> = new Array<TreeSelectModel<Category>>();


            categories.forEach((cat) => {
                if (cat !== undefined && cat !== null) {

                    const selecteCat = this.item.productCategories.find(a => a.categoryId == cat.id);

                    if (selecteCat !== undefined) {
                        this.productCategoryNames.push(selecteCat.category?.name ?? '');
                    }
                    const treeSelectViewModel: TreeSelectModel<Category> = {
                        item: cat,
                        isSelected: selecteCat !== undefined,
                        children: this.buildTreeSelectModel(cat.subCategories),

                    };
                    treeSelect.push(treeSelectViewModel);
                }
            });

            return treeSelect;
        }



        // dialog ma możliwość lokalnej zmiany property
        @Watch('show')
        async onPropertyShowChanged(value: boolean, oldValue: boolean) {
            if (value == false) {
                this.closeDialog();
            }
        }

        closeDialog() {
            this.$emit("dialog-closed");
        }

        // ///gdy zmienia się property przez parenta
        @Watch('isShow')
        onPropertyIsShowChanged(value: boolean, oldValue: boolean) {
            this.show = value;

        }

        @Watch('productId')
        async onPropertyCategoryIdChanged(value: number, oldValue: number) {
            await this.loadItem();
          
        }

        get currentTitle() {
            if (this.item.id === 0) {
                return "Utwórz nowy"
            }
            else
                return "Edytuj"
        }
    }
</script>