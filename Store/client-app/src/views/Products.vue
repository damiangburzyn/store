<!--//self.$route.params.id-->

<template>
    <div id="home" class="home">
        <!--<img alt="Vue logo" src="../assets/logo.png">
        <HelloWorld msg="Welcome to Your Vue.js App" />-->
        <!--<CategoryCard  :item="cat"></CategoryCard>-->


        <v-row v-if="categories.length > 0" v-for="(group, i) in categoryGroups">

            <CategoryCard v-for="category in categories.slice(i * itemsPerRow, (i + 1) * itemsPerRow)" :item="category"></CategoryCard>

        </v-row>

    </div>

</template>

<script lang="ts">
    // @ is an alias to /src

    import { Component, Vue } from 'vue-property-decorator';
    import { Product } from '@/store/modelsData'
    import { productService } from "@/store/api";
    import productCard from "@/components/Cards/productCard.vue"


    import Categories from '../components/AdminPanel/Categories.vue';

    @Component({
        components: {
            ProductCard: productCard,

        },
    })
    export default class Home extends Vue {

        public products = new Array<Product>();
       

        private itemsPerRow = 3;
        async getCategories() {

            let catId = parseInt(this.$route.params.id);

            this.products = await productService.ProductsInCategory(catId);
           
        }

        getItem(i: number) {
            let item = this.products[i]
            return item ;
        }

        async created() {
            await this.getCategories();
        }


        get categoryGroups() {
            let res = Array.from(Array(Math.ceil(this.products.length / this.itemsPerRow)).keys())
            return res;
        }
    }
</script>
<style lang="scss">
    #home {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
