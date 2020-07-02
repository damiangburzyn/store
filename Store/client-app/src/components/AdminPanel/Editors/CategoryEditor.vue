<template>



    <div class="text-center">
        <v-dialog scrollable v-model="Show"
                  persistent
                  width="500">
            <!--<template v-slot:activator="{ on }">
            <v-btn color="red lighten-2"
                   dark
                   v-on="on">
                Click Me
            </v-btn>
        </template>-->

            <v-card>
                <v-card-title class="headline grey lighten-2"
                              primary-title>
                    {{ currentTitle }}
                </v-card-title>

                <v-card-text>
                    <v-text-field label="Nazwa"
                                  v-model="Item.name"></v-text-field>
                    <!--v-bind:src="Item.logo.url"-->

                    <v-img v-if=" Item.logo !== null && Item.logo.url !== '' " class="align-content-center" v-bind:src="Item.logo.url" aspect-ratio="1.7" contain></v-img>

                    <v-file-input :rules="ImageRules"
                                  accept="image/png, image/jpeg, image/bmp"
                                  :placeholder="imagePlaceHolder()"
                                  @change="onFilePicked"
                                  prepend-icon="mdi-camera"
                                  label="Obraz kategorii">
                    </v-file-input>

                    <!--<v-select v-model="SelectedParentCategory"
                :hint="`${SelectedParentCategory.Text}, ${SelectedParentCategory.Value}`"
                :items="SelectCategories"
                item-text="Text"
                item-value="Value"
                label="Select"
                persistent-hint
                return-object
                single-line></v-select>-->


                    <v-text-field label="Kategoria nadrzędna" readonly
                                  v-model="ParentCategoryName"
                                  @click="ShowParentCategoryDialog = true"
                                  :clearable="true"
                                  @click:clear="onParentCategoryDelete()">

                    </v-text-field>


                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                    <v-btn color="primary"
                           text
                           @click="Show = false">
                        Anuluj
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-btn color="primary"
                           text
                           @click="Save()">
                        Zapisz
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>


        <v-dialog scrollable v-model="ShowParentCategoryDialog"
                  width="500">
            <v-card>
                <v-card-title class="headline grey lighten-2"
                              primary-title>
                    Wybierz kategorię
                </v-card-title>
                <v-card-text>

                    <v-treeview :items="Categories"
                                item-children="subCategories"
                                item-key="id"
                                item-text="name">

                        <template v-slot:label="{ item, open }">
                            {{item.name}}

                            <v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                                   title="Wybierz"
                                   elevation="0"
                                   v-on:click="setParentCategory(item)">

                            </v-btn>


                        </template>
                    </v-treeview>
                </v-card-text>

                <v-card-actions>
                    <v-btn color="primary"
                           text
                           @click="ShowParentCategoryDialog = false">
                        Anuluj
                    </v-btn>
                    <v-spacer></v-spacer>

                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>   
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { SelectItem, Content } from '@/store/models';
    import { Category} from '@/store/modelsData';
    import { categoryService } from "@/store/api";
    @Component
    export default class CategoryEditor extends Vue {
       
        @Prop(Number) readonly CategoryId!: number;
        @Prop(Boolean) readonly IsShow!: boolean;
        @Prop() readonly Categories!: Array<Category>;
        public ShowParentCategoryDialog = false;
        private Item: Category = this.getEmptyCategory();
        public Show = this.IsShow;
       
        public ParentCategoryName: string = '';
        public ImageRules: [Function] = [
            (value: File) => !value || value.size < 2000000 || 'Rozmiar obrazu powinien być poniżej 2 MB!',
        ];
        //public SelectCategories: SelectItem<number>[] = [
        //    { Text: "Category1", Value: 1 },
        //    { Text: "Category2", Value: 2 },
        //    { Text: "Category3", Value: 3 }
        //];

        SelectedParentCategory: SelectItem<number | null> = { Text: "----------", Value: null }


        async  Save() {

            try {
                if (this.Item != null && this.Item?.id === 0) {
                    await categoryService.create(this.Item);
                }
                else {
                    await categoryService.update(this.Item);
                }
                this.$emit('saved-item');
            }
            catch (ex) {
                console.error(ex);
            }
        }

       async LoadItem() {

            if (this.CategoryId == 0) {
                this.Item = this.getEmptyCategory();
                this.ParentCategoryName = '';               
            }
            else {

                var data = await categoryService.Get(this.CategoryId);
                if (data.logo === null) {
                    data.logo = this.getEmptyLogo();
                }

                this.Item = data;
            }
        }

        imagePlaceHolder() {
            if (this.Item.logo.name !== '') {
                return this.Item.logo.name;

            }
            else return 'Wybierz obraz';
        }

        getEmptyLogo(): Content {
            const img: Content = {
               name: '',
               url: '',
               data: '',
           };
             return img;
        }

        getEmptyCategory(): Category {
            const cat: Category = {
                id: 0,
                name: '',
                shortName: '',
                sortOrder: 0,
                logo: this.getEmptyLogo(),
                parentCategoryId: null,
                subCategories: []
            };
            return cat
        }

        onFilePicked(file: File) {
            let self = this;
            self.Item.logo = self.getEmptyLogo();

            if (file !== undefined) {
                self.Item.logo.name = file.name
                if (this.Item.logo.name.lastIndexOf('.') <= 0) {
                    return
                }
                const fr = new FileReader()
                fr.readAsDataURL(file)
                fr.addEventListener('load', (e) => {
                    self.Item.logo.data = fr.result as string || '';
                    self.Item.logo.url = URL.createObjectURL(file);  // this is an image file that can be sent to server...
                    //console.log(e);
                })
            } else {
                this.Item.logo.name = '';
                this.Item.logo.data = '';
                this.Item.logo.url = '';
            }
        }

        setParentCategory(item: Category) {
            this.ParentCategoryName = item.name;
            this.Item.parentCategoryId = item.id;
            this.ShowParentCategoryDialog = false;
        }

        onParentCategoryDelete() {
            this.ParentCategoryName = '';
            this.Item.parentCategoryId =null;
        }

        // dialog ma możliwość lokalnej zmiany property
        @Watch('Show')
        onPropertyShowChanged(value: boolean, oldValue: boolean) {
            if (value == false) {
                this.closeDialog();
            }       
        }
       
        closeDialog() {
            this.$emit("dialog-closed");
        }

        ///gdy zmienia się property przez parenta
        @Watch('IsShow')
        onPropertyIsShowChanged(value: boolean, oldValue: boolean) {
            this.Show = value;
           
        }

        @Watch('CategoryId')
        onPropertyCategoryIdChanged(value: number, oldValue: number) {
            this.LoadItem(); 
        }

        get currentTitle() {
            if (this.Item.id === 0) {
                return "Utwórz nowy"
            }
            else
                return "Edytuj"
        }
    }
</script>