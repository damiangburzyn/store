<template>
    <div>
        Opcje dostawy


        <button @:click="SaveProduct"> Zapisz</button>
    </div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { Product } from '@/store/models';
@Component
export default class DeliveryOptions extends Vue {
 private msg!: string;
 public ProductEdit: Product|null = null;

    SaveProduct() {
        console.log(this.ProductEdit);
        this.ProductEdit = {} as Product;
    };
}
</script>