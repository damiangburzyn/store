<template>
    <v-data-table :headers="headers"
                  :items="desserts"
                  sort-by="calories"
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
                <v-dialog v-model="dialog" max-width="500px">
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn color="primary"
                               dark
                               class="mb-2"
                               v-bind="attrs"
                               v-on="on">New Item</v-btn>
                    </template>
                    <v-card>
                        <ProductEditor></ProductEditor>
                    </v-card>
                </v-dialog>
            </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
            <v-icon small
                    class="mr-2"
                    @click="editItem(item)">
                mdi-pencil
            </v-icon>
            <v-icon small
                    @click="deleteItem(item)">
                mdi-delete
            </v-icon>
        </template>
        <template v-slot:no-data>
            <v-btn color="primary" @click="initialize">Reset</v-btn>
        </template>

        <template v-slot:item.image="{ item }">
            <v-img  v-if="item.image !== null &&  item.image.url !== ''" :src="item.image.url"
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
  private msg!: string;
        productEdit: Product | null = null;
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
            { text: 'Poprzednia cena', value: 'PreviousPrice' },
            { text: 'Bestseller', value: 'isBestseller' },
            { text: 'Ilość sztuk', value: 'count' },
            //{ text: 'Obraz', value: 'image' },
            { text: 'Actions', value: 'actions', sortable: false },
        ];

    saveProduct() {
     
        console.log(this.productEdit);
        this.productEdit = {} as Product;
        }
        onItemsPerPageChange(itemsPerPage :number) { }
        onPageChange(page: number) { }

        async search() {
         const res =    await productService.find(this.tableProps.pageNo, this.tableProps.rowsPerPage, this.queryString)
            this.products = res;
        }
}
</script>