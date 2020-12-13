<template>

    <v-card v-on:click="onClick" class="mx-auto my-12"
              min-width="100"
            max-width="374">
        <v-img min-width="10"  height="250"
               :src="item.logo.url"></v-img>
        <!--<v-img height="250" src="https://cdn.vuetifyjs.com/images/cards/cooking.png"></v-img>-->

        <v-card-title>{{item.name}}</v-card-title>
        <v-divider class="mx-4"></v-divider>
        <v-card-text>


            <div class="my-4 subtitle-1">
                $ • Italian, Cafe
            </div>

            <div>Small plates, salads & sandwiches - an intimate setting with 12 indoor seats plus patio seating.</div>
        </v-card-text>

        <v-divider class="mx-4"></v-divider>

        <v-card-actions>
            <v-btn color="deep-purple lighten-2"
                   text
                   @click.stop="addToBasket">
                Reserve
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue , Prop} from 'vue-property-decorator';
    import { Category } from '@/store/modelsData'

    @Component
    export default class ProductCard extends Vue {
        @Prop({ default: new Category() }) readonly item!: Category;

        onClick() {
            console.log('test')
        }

        addToBasket() {

        }
    }

</script>