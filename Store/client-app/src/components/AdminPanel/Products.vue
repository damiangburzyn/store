<template>
 
    <v-data-table :headers="headers"
                  :items="products"
                  :items-per-page="tableProps.rowsPerPage"
                  :page="tableProps.pageNo"
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
                <v-dialog v-model="isEditorEnabled" max-width="500px">
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn color="primary"
                               dark
                               class="mb-2"
                               v-bind="attrs"
                               v-on="on">New Item</v-btn>
                    </template>
                    <v-card>
                        <ProductEditor  :productId="selectedProductId" @dialog-closed="onEditorClosed" @saved-item="onEditorSaved"></ProductEditor>
                    </v-card>
                </v-dialog>
            </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
            <v-icon small
                    class="mr-2"
                    @click="editItem(item.id)">
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
    import { Product } from '@/store/models';
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
        selectedProductId: Number | null = null;
        products: Product[] = [];
        tableProps = {
            rowsPerPage:10,
            pageNo: 1,
            total: 0
        };
        queryString: string = '';
        headers = [

            {
                text: 'Nazwa', align: 'start',
                sortable: false, value: 'name' },
            { text: 'Cena', value: 'currentPrice' },
            { text: 'Poprzednia cena', value: 'previousPrice' },
            { text: 'Bestseller', value: 'isBestseller' },
            { text: 'Ilość sztuk', value: 'count' },
            //{ text: 'Obraz', value: 'image' },
            { text: 'Actions', value: 'actions', sortable: false },
        ];

    saveProduct() {
     
        console.log(this.selectedProductId);
      
        }
        onItemsPerPageChange(itemsPerPage :number) { }
        onPageChange(page: number) { }

        async search() {
         const res =    await productService.search(this.tableProps.pageNo, this.tableProps.rowsPerPage, this.queryString)
            this.products = res.data;
            this.tableProps.pageNo = res.currentPage;
            this.tableProps.total = res.total;
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

        edit(id: number) {
            this.selectedProductId = id;
            this.isEditorEnabled = true;
        }
}
</script>