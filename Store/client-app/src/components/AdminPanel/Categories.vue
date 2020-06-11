<template>
    <div>
      
               
            <br />

            <div class="fullopacity">
                <v-btn color="blue lighten-2"
                       dark
                       right
                       absolute
                       v-on:click="AddNew">
                    Dodaj
                </v-btn>
                <CategoryEditor :IsShow="IsEditorEnabled" :CategoryId="SelectedCategoryId" :Categories="Items" @dialog-closed="onEditorClosed" @saved-item="onEditorSaved" />
                <ConfirmationDialog :IsShow="IsRemoveConfirmation"
                                    :Message="RemoveMessage" @Confirm="RemoveConfirmed" @Cancel="RemoveCanceled" />

                <br />
                <v-treeview :items="Items"
                            item-children="subCategories"
                            item-key="id"
                            item-text="name">

                    <template v-slot:label="{ item}">
                        {{item.name}}

                        <v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                               title="Edytuj"
                               elevation="0"
                               v-on:click="Edit(item.id)">
                            <v-icon class="body-2">mdi-pencil</v-icon>
                        </v-btn>


                        &nbsp;
                        <v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                               title="Usuń"
                               elevation="0"
                               v-on:click="confirmRemove(item.Id)">
                            <v-icon class="body-2">mdi-delete-forever-outline</v-icon>
                        </v-btn>

                    </template>

                </v-treeview>
            </div>
   
    </div>

</template>


<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import CategoryEditor from "@/components/AdminPanel/Editors/CategoryEditor.vue"
    import ConfirmationDialog from "@/components/AdminPanel/Editors/ConfirmationDialog.vue"

    import { categoryService } from "@/store/api";
    import { Category } from '@/store/modelsData';
    @Component({
        components: {
            CategoryEditor: CategoryEditor,
            ConfirmationDialog: ConfirmationDialog
        },
    })
    export default class Categories extends Vue {

        IsEditorEnabled = false;
        IsRemoveConfirmation = false;
        ToRemove = 0;
        SelectedCategoryId = 0;
        Items: Array<Category> = [];
        RemoveMessage = "Czy na pewno chcesz usunąć kategorię? Dane zostaną trwale usunięte";

        async LoadTree() {      
            const categories = await categoryService.Tree()
            this.Items = categories;
        }

   

        AddNew() {           
            this.SelectedCategoryId = 0;
            this.IsEditorEnabled = true;
        }

        Edit(id: number) {            
            this.SelectedCategoryId = id;
            this.IsEditorEnabled = true;
        }

        Remove(id: number) {          
            this.SelectedCategoryId = id;
            this.IsEditorEnabled = true;
        }

        onEditorClosed() {
            this.IsEditorEnabled = false;
            this.SelectedCategoryId = 0;
        }

       async onEditorSaved() {
           await this.LoadTree();
           this.onEditorClosed();

        }


        confirmRemove(id: number) {
            this.ToRemove = id;
            this.IsRemoveConfirmation = true;
        }

        RemoveConfirmed() {

            this.IsRemoveConfirmation = false;
        }

        RemoveCanceled() {
            this.IsRemoveConfirmation = false;
        }

        async  created() {
            await this.LoadTree();
        }
    }

</script>



<style >
    .button-mini {
        width: 20px !important;
        height: 20px !important;
        font-size: 15px !important;
    }

  /*  .card-trasparent {
        opacity:0.12;
    }

    .fullopacity {
    opacity:1;
    }*/
</style>