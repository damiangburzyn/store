<template>

    <v-card v-on:click="onClick" class="mx-auto my-12"
              min-width="100"
            max-width="374">
        <v-img v-if="item.images.length > 0" min-width="10"  height="250"
               :src="getImageUrl()"></v-img>
        <!--<v-img height="250" src="https://cdn.vuetifyjs.com/images/cards/cooking.png"></v-img>-->

        <v-card-title>{{item.name}}</v-card-title>
        <v-divider class="mx-4"></v-divider>
        <v-card-text>


            <div class="my-4 subtitle-1">
                • $ {{item.currentPrice}}
            </div>

            <div>{{item.description}}</div>
        </v-card-text>

        <v-divider class="mx-4"></v-divider>

        <v-card-actions>
            <v-spacer></v-spacer>  <v-btn color="deep-purple lighten-2"
                                          text
                                          @click.stop="addToBasket">
                Dodaj do koszyka
            </v-btn>
            <v-spacer></v-spacer>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue , Prop} from 'vue-property-decorator';
    import { Product } from '@/store/modelsData'
    import cartModule from '@/store/Modules/Cart'


    @Component
    export default class ProductCard extends Vue {
        @Prop({ default: new Product() }) readonly item!: Product;

        onClick() {
            console.log('test')
        }

        getImageUrl() {
            if (this.item.images.length > 0) {
                return this.item.images[0].url;
            }
        }

       async addToBasket() {
            await cartModule.addToCard(this.item);
        }
    }

</script>