<template>
    <div>

        <v-btn color="blue lighten-2"
               dark
               v-on:click="AddNew">
            Dodaj
        </v-btn>
        <CategoryEditor :IsShow="IsEditorEnabled" :CategoryId="SelectedCategoryId" @dialog-closed="onEditorClosed" />
        <ConfirmationDialog :IsShow="IsRemoveConfirmation"
                            :Message="RemoveMessage" @Confirm="RemoveConfirmed" @Cancel="RemoveCanceled" />

        <br />
        <v-treeview :items="Items"
                    item-children="subCategories"
                    item-key="id"
                    item-text="name">

            <template v-slot:label="{ item, open }">
                {{item.name}}

                <v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                       title="Edytuj"
                       elevation="0"
                       v-on:click="Edit(item.id)">
                    <v-icon class="body-2">mdi-pencil</v-icon>
                </v-btn>


                &nbsp;
                <v-btn x-small  class="button-mini" color="blue lighten-2" fab dark
                       title="Usuń"
                       elevation="0"
                       v-on:click="confirmRemove(item.Id)">
                    <v-icon class="body-2">mdi-delete-forever-outline</v-icon>
                </v-btn>

            </template>

        </v-treeview>

    </div>

</template>


<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import CategoryEditor from "@/components/AdminPanel/Editors/CategoryEditor.vue"
    import ConfirmationDialog from "@/components/AdminPanel/Editors/ConfirmationDialog.vue"

    import { CategoryService } from "@/store/api";
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
        ToRemove: number = 0;
        SelectedCategoryId = 0;
        Items: Array<Category> = [];
        RemoveMessage = "Czy na pewno chcesz usunąć kategorię? Dane zostaną trwale usunięte";

        async LoadTree() {
            const self = this;
            var categories = await CategoryService.Tree()
            self.Items = categories;
        }

        EditorCompleted() {
            const self = this;
            self.IsEditorEnabled = false;
        }

        AddNew() {
            const self = this;
            self.SelectedCategoryId = 0;
            self.IsEditorEnabled = true;
        }

        Edit(id: number) {
            const self = this;
            self.SelectedCategoryId = id;
            self.IsEditorEnabled = true;
        }

        Remove(id: number) {
            const self = this;
            self.SelectedCategoryId = id;
            self.IsEditorEnabled = true;
        }


        onEditorClosed(value: any) {

            this.IsEditorEnabled = false;
            this.SelectedCategoryId = 0;
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
</style>