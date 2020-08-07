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

                <v-textarea name="input-7-1"
                            filled
                            v-model="item.description"
                            label="Opis"
                            auto-grow
                            value=""></v-textarea>

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

                var data = await service.get(this.itemId);
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

        @Watch('itemId')
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