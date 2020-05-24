<template>
    <fieldset> <input type="text" v-model="{{Title}}" /></fieldset>
    <fieldset> <input type="image" v-model="{{Image}}" /></fieldset>
    <fieldset> <input type="text" v-model="{{Description}}" /></fieldset>
    <fieldset> <input type="number" v-model="{{CurrentPrice}}" /></fieldset>
    <fieldset> <input type="number" v-model="{{PrevPrice}}" /></fieldset>
    <fieldset> <input /></fieldset>


    <button @:click="SaveProduct"> Zapisz</button>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { Product } from '@/store/models';
@Component
export default class HelloWorld extends Vue {
  @Prop() private msg!: string;
  @Prop() public ProductEdit: Product|null = null;

    SaveProduct() {
        console.log(this.ProductEdit);
        this.ProductEdit = {} as Product;
    };
}
</script>