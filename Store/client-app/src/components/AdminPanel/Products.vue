<template>
    <div>

      

    </div>
</template>


<script lang="ts">
import { Component,  Vue } from 'vue-property-decorator';
import { Product } from '@/store/models';
@Component
export default class Products extends Vue {
  private msg!: string;
    ProductEdit: Product|null = null;

    SaveProduct() {
     
        console.log(this.ProductEdit);
        this.ProductEdit = {} as Product;
    }
}
</script>