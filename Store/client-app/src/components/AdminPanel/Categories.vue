<template>
    <div>

        <v-btn color="blue lighten-2"
               dark
               v-on:click="AddEditor">
            Dodaj
        </v-btn>
        <CategoryEditor :IsShow="IsEditorEnabled" :CategoryId="SelectedCategoryId" @dialog-closed="onEditorClosed" />
        <br />
        <v-treeview :items="Items"
                    :item-children="SubCategories"
                    :item-key="Id"
                    :item-text="Name"
                    ></v-treeview>

    </div>
   
</template>


<script lang="ts">
    import { Component,  Vue } from 'vue-property-decorator';
    import CategoryEditor from "@/components/AdminPanel/Editors/CategoryEditor.vue"
    import { CategoryService } from "@/store/api";
    import { Category } from '@/store/modelsData';
    @Component({
        components: {
            CategoryEditor: CategoryEditor
        },
    })
    export default class Categories extends Vue {

        IsEditorEnabled= false;
        SelectedCategoryId = 0;
        Items: Category[] = [];


        async LoadTree() {
           const self = this;
           var categories = await CategoryService.Tree()
           self.Items = categories;
        }

        EditorCompleted() {
            const self = this;
            self.IsEditorEnabled = false;
        }

        AddEditor() {
            const self = this;
            self.SelectedCategoryId = 0;
            self.IsEditorEnabled = true;
        }

        onEditorClosed(value:any) {

            this.IsEditorEnabled = false;
            this.SelectedCategoryId = 0;
        }


      async  created() {
           await this.LoadTree();
        }
    }

</script>