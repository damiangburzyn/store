<template>

    <v-card class="mx-auto my-12"
            min-width="300"
            max-width="374">
        <v-img v-if="item.images.length > 0" min-width="10" height="250"
               :src="getImageUrl()"></v-img>
        <v-card-title>{{item.name}}</v-card-title>
        <v-divider class="mx-4"></v-divider>
        <v-card-text>
            <div class="my-4 subtitle-1">
                $ {{item.currentPrice}}zł
            </div>
            <br />
            <div>
                <v-slider label="Ilość"
                          :max="200"
                          min="1"
                          thumb-label="always"
                          :thumb-color="'cyan'"
                          v-model="count"></v-slider>
            </div>
            <div>{{item.description}}</div>
        </v-card-text>

        <v-divider class="mx-4"></v-divider>

        <v-card-actions>
            <v-spacer></v-spacer>  <v-btn color="cyan lighten-4"
                                          primary
                                          @click.stop="addToBasket">
                Dodaj do koszyka
            </v-btn>
            <v-spacer></v-spacer>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import { Product } from '@/store/modelsData'
    import cartModule from '@/store/Modules/Cart'


    @Component
    export default class ProductCard extends Vue {
        @Prop({ default: new Product() }) readonly item!: Product;

        private count = 1;

        //countRules: Array<Function> = [
        //    (v: number) => v < 1 || 'wartośc musi być większa od 0'
        //];


    

        getImageUrl() {
            if (this.item.images.length > 0) {
                return this.item.images[0].url;
            }
        }

        async addToBasket() {
            await cartModule.addToCard({ product :this.item,  count : this.count});
        }
    }

</script>