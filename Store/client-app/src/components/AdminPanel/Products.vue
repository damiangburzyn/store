<template>
 
    <v-data-table :headers="headers"
                  :items="products"
                  :items-per-page="tableProps.rowsPerPage"
                  :page="tableProps.pageNo"
                  :server-items-length="tableProps.total"
                  @update:page="onPageChange"
                  @update:items-per-page="onItemsPerPageChange"
                  class="elevation-1">
        <template v-slot:top>
            <v-toolbar flat color="white">
                <v-toolbar-title>Produkty</v-toolbar-title>
                <v-divider class="mx-4"
                           inset
                           vertical></v-divider>
                <v-spacer></v-spacer>


                <v-btn color="blue lighten-2"
                       class="mr-3"
                       dark
                       v-on:click="addNew">
                    Dodaj
                </v-btn>

                <v-card>
                    <ProductEditor :isShow="isEditorEnabled" :productId="selectedProductId" @dialog-closed="onEditorClosed" @saved-item="onEditorSaved"></ProductEditor>
                </v-card>

            </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
            <v-icon small
                    class="mr-2"
                    @click="edit(item.id)">
                mdi-pencil
            </v-icon>
            <v-icon small
                    @click="remove(item.id)">
                mdi-delete
            </v-icon>
        </template>
        <!--<template v-slot:no-data>
            <v-btn color="primary" @click="initialize">Reset</v-btn>
        </template>-->

        <template v-slot:item.image="{ item }">
            <v-img v-if="item.image !== null &&  item.image.url !== ''" :src="item.image.url"
                   :lazy-src="item.image.url"
                   aspect-ratio="1"
                   class="grey lighten-2">
            </v-img>
        </template>
    </v-data-table>
</template>


<script lang="ts">
import { Component,  Vue } from 'vue-property-decorator';
    import { Product } from '@/store/modelsData';
    //import DataTableSearchViewModel from '@/store/modelsData';
    import ProductEditor from "@/components/AdminPanel/Editors/ProductEditor.vue"
    import ConfirmationDialog from "@/components/AdminPanel/Editors/ConfirmationDialog.vue"

    import { productService } from "@/store/api";
    import { Category } from '@/store/modelsData';
    @Component({
        components: {
            ProductEditor: ProductEditor,
            ConfirmationDialog: ConfirmationDialog
        },
    })
export default class Products extends Vue {


        isEditorEnabled = false;
        
        msg!: string;
        selectedProductId = 0;
        products: Product[] = [];
        tableProps = {
            rowsPerPage:10,
            pageNo: 1,
            total: 0
        };
        queryString = '';
        headers = [
            {
                text: 'Nazwa', align: 'start',
                sortable: false, value: 'name' },
            { text: 'Cena', value: 'currentPrice' },
            { text: 'Poprzednia cena', value: 'previousPrice' },
            { text: 'Bestseller', value: 'isBestseller' },
            //{ text: 'Ilość sztuk', value: 'count' },
            //{ text: 'Obraz', value: 'image' },
            { text: 'Działania', value: 'actions', sortable: false },
        ];

      async  onItemsPerPageChange(itemsPerPage: number) {
          this.tableProps.pageNo = 1;
          this.tableProps.rowsPerPage = itemsPerPage;
          await this.search();
        }
     

        async search() {
         const res =    await productService.search(this.tableProps.pageNo, this.tableProps.rowsPerPage, this.queryString)
            this.products = res.data;
            this.tableProps.pageNo = res.currentPage;
            this.tableProps.total = res.total;
        }

        async  onPageChange(page: number) {

            this.tableProps.pageNo = page;
            await this.search();

        }

      async  created() {
           await this.search();
        }

    

        onEditorClosed() {
            this.isEditorEnabled = false;
            this.selectedProductId = 0;
        }

      async  onEditorSaved() {
            await this.search();
            this.onEditorClosed();
        }

        async remove(id: number) {
            await productService.destroy(id);
        }

        addNew() {
            this.selectedProductId = 0;
            this.isEditorEnabled = true;
        }

        edit(id: number) {
            this.selectedProductId = id;
            this.isEditorEnabled = true;
        }
}
</script>