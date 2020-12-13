<template>

    <v-card @click="goToCategoryProducts(item.id)" class="mx-auto my-12"
              min-width="100"
            max-width="374">
        <v-img v-if="item !== null && item.logo !== null && item.logo.url !== null"  min-width="10"  height="250"
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
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue , Prop} from 'vue-property-decorator';
    import { Category } from '@/store/modelsData'
    import router from '@/router/index'
    @Component
    export default class CategoryCard extends Vue {
        @Prop({ default: new Category() }) readonly item!: Category;
        private router = router;

        async goToCategoryProducts(id: number) {
            await router.push(  'Products/'+ id.toString() );
        }
    }

</script>