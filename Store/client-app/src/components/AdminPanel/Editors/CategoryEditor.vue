<template>



    <div class="text-center">
        <v-dialog v-model="Show"
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
                                  v-model="Item.Name"></v-text-field>
                    <span class="caption grey--text text--darken-1">
                        This is the email you will use to login to your Vuetify account
                    </span>


                    <v-file-input :rules="ImageRules"
                                  accept="image/png, image/jpeg, image/bmp"
                                  placeholder="Wybierz obraz"
                                  @change="onFilePicked"
                                  prepend-icon="mdi-camera"
                                  label="Obraz kategorii"></v-file-input>

                    <v-select v-model="SelectedParentCategory"
                              :hint="`${SelectedParentCategory.Text}, ${SelectedParentCategory.Value}`"
                              :items="SelectCategories"
                              item-text="Text"
                              item-value="Value"
                              label="Select"
                              persistent-hint
                              return-object
                              single-line></v-select>
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
    </div>   
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { SelectItem } from '@/store/models';
    import { Category} from '@/store/modelsData';
    import { CategoryService } from "@/store/api";
    @Component
    export default class CategoryEditor extends Vue {
        private msg!: string;
        @Prop(Number) readonly CategoryId!: number;
        @Prop(Boolean) readonly IsShow!: boolean;
        private Item: Category = this.GetEmpty();
        public Show = this.IsShow;
        public LocalCatId= this.CategoryId;
        public ImageRules: [string | boolean |any] = [
            (value: string|boolean|any) => !value || value.size < 2000000 || 'Avatar size should be less than 2 MB!',
        ];
        public SelectCategories: SelectItem<number>[] = [
            { Text: "Category1", Value: 1 },
            { Text: "Category2", Value: 2 },
            { Text: "Category3", Value: 3 }
        ];

        SelectedParentCategory: SelectItem<number | null> = { Text: "----------", Value: null }




        async  Save() {
            if (this.Item != null && this.Item?.Id === 0) {
                await CategoryService.create(this.Item);
            }
            else {
                await CategoryService.update(this.Item);
            }
        }


       async LoadItem() {

            if (this.CategoryId == 0) {
                this.Item = this.GetEmpty();
            }
            else {

                var data = await CategoryService.Get(this.CategoryId);
                this.Item = data;
            }


        }
        GetEmpty(): Category {

            const cat: Category = {
                Id: 0,
                Name: '',
                ShortName: '',
                SortOrder: 0,
                Logo: {
                    Name: '',
                    Url: '',
                    Data: '',
                },
                ParentCategoryId: null,
                SubCategories: []
            };

            return cat
        }

        onFilePicked(file: File) {
            let self = this;
            if (file !== undefined) {
                self.Item.Logo.Name = file.name
                if (this.Item.Logo.Name.lastIndexOf('.') <= 0) {
                    return
                }
                const fr = new FileReader()
                fr.readAsDataURL(file)
                fr.addEventListener('load', () => {
                    self.Item.Logo.Data = fr.result as string || '';
                    self.Item.Logo.Url = file.name // this is an image file that can be sent to server...
                })
            } else {
                this.Item.Logo.Name = '';
                this.Item.Logo.Data = '';
                this.Item.Logo.Url = '';
            }
        }

        // dialog ma możliwość lokalnej zmiany property
        @Watch('Show')
        onPropertyChanged(value: boolean, oldValue: boolean) {
            if (value == false) {
                this.closeDialog();
            }
            else {

                this.LoadItem();
            }

            alert("CategoryId changed");
        }
       
        closeDialog() {
            this.$emit("dialog-closed");
        }

        ///gdy zmienia się property przez parenta
        @Watch('IsShow')
        onPropertyChangedtwo(value: boolean, oldValue: boolean) {
            this.Show = value;
            alert("IsShow changed");
        }

        get currentTitle() {
            if (this.Item.Id === 0) {
                return "Utwórz nowy"
            }
            else
                return "Edytuj"
        }
    }
</script>