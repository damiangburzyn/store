<template>

    <v-dialog v-model="show"
              persistent
              max-width="500px">
        <v-card>
            <v-card-title class="headline grey lighten-2"
                          primary-title>
                {{ currentTitle }}
            </v-card-title>

            <v-card-text>
                <v-text-field label="Nazwa"
                              v-model="item.name"></v-text-field>

              



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

    </v-dialog>
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { SelectItem, Content } from '@/store/models';
    import { DeliveryMethod } from '@/store/modelsData';
    import { deliveryMehodService as service } from "@/store/api";
    @Component
    export default class DeliveryOptionEditor extends Vue {
        private msg!: string;
        @Prop(Number) readonly itemId!: number;
        @Prop(Boolean) readonly isShow!: boolean;
      

        // public ShowParentCategoryDialog = false;
        private item: DeliveryMethod = new DeliveryMethod();
        public show = this.isShow;

     
     

        async  save() {

            try {
                if (this.item != null && this.item?.id === 0) {
                    await service.create(this.item);
                }
                else {
                    await service.update(this.item);
                }
                this.$emit('saved-item');
            }
            catch (ex) {
                console.error(ex);
            }
        }

        async loadItem() {

            if (this.itemId == 0) {
                this.item = new DeliveryMethod();

            }
            else {

                var data = await service.Get(this.itemId);
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

     


      
        onImagesupdateError(e: any) {
            console.log(e);
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

        // dialog ma możliwość lokalnej zmiany property
        @Watch('show')
        onPropertyShowChanged(value: boolean, oldValue: boolean) {
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
        onPropertyCategoryIdChanged(value: number, oldValue: number) {

            this.loadItem();
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