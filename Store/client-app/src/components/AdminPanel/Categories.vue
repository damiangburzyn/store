<template>
    <div>
        <div class="fullopacity">
            <v-row>
                <v-col class="d-flex flex-row-reverse"
                       color="grey lighten-2"
                       flat
                       tile>
                    <v-btn color="blue lighten-2"
                           class="mr-3"
                           dark
                           v-on:click="addNew">
                        Dodaj
                    </v-btn>
                </v-col>
            </v-row>
            <CategoryEditor :IsShow="isEditorEnabled" :CategoryId="selectedCategoryId" :Categories="items" @dialog-closed="onEditorClosed" @saved-item="onEditorSaved" />
            <ConfirmationDialog :IsShow="isRemoveConfirmation"
                                :Message="removeMessage" @Confirm="removeConfirmed" @Cancel="removeCanceled" />

            <br />

            <!--<v-dialog v-model="isEditorEnabled" max-width="500px">
                <template v-slot:activator="{ on, attrs }">
                    <v-btn color="primary"
                           dark
                           class="mb-2"
                           v-bind="attrs"
                           v-on="on">New Item</v-btn>
                </template>
                <v-card>
                    <CategoryEditor :IsShow="isEditorEnabled" :CategoryId="selectedCategoryId" :Categories="items" @dialog-closed="onEditorClosed" @saved-item="onEditorSaved" />
                </v-card>
            </v-dialog>-->

            <v-treeview :items="items"
                        item-children="subCategories"
                        item-key="id"
                        item-text="name">




                <template v-slot:label="{ item}">
                    {{item.name}}

                    <v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                           title="Edytuj"
                           elevation="0"
                           v-on:click="edit(item.id)">
                        <v-icon class="body-2">mdi-pencil</v-icon>
                    </v-btn>


                    &nbsp;
                    <v-btn x-small class="button-mini" color="blue lighten-2" fab dark
                           title="Usuń"
                           elevation="0"
                           v-on:click="confirmRemove(item.id)">
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
            ConfirmationDialog: ConfirmationDialog,

        },
    })
    export default class Categories extends Vue {

        isEditorEnabled = false;
        isRemoveConfirmation = false;
        toRemove = 0;
        selectedCategoryId = 0;
        items: Array<Category> = [];
        removeMessage = "Czy na pewno chcesz usunąć kategorię? Dane zostaną trwale usunięte";

        async LoadTree() {
            const categories = await categoryService.tree()
            this.items = categories;
        }



        addNew() {
            this.selectedCategoryId = 0;
            this.isEditorEnabled = true;
        }

        edit(id: number) {
            this.selectedCategoryId = id;
            this.isEditorEnabled = true;
        }

        async Remove(id: number) {
            await categoryService.destroy(id).then(async () => {
                await this.LoadTree();
            });

        }

        onEditorClosed() {
            this.isEditorEnabled = false;
            this.selectedCategoryId = 0;
        }

        async onEditorSaved() {
            await this.LoadTree();
            this.onEditorClosed();
        }

        confirmRemove(id: number) {
            this.toRemove = id;
            this.isRemoveConfirmation = true;
        }

        removeConfirmed() {
            this.Remove(this.toRemove);
            this.isRemoveConfirmation = false;
        }

        removeCanceled() {
            this.isRemoveConfirmation = false;
        }

        async created() {
            await this.LoadTree();
        }
    }

</script>



<style>
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