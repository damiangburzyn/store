<!--//self.$route.params.id-->

<template>
    <div id="home" class="home">
        <div v-if="products.length > 0">
            <v-row v-for="(group, i) in categoryGroups" :key="i">

                <ProductCard v-for="(product, index) in products.slice(i * itemsPerRow, (i + 1) * itemsPerRow)" :item="product" :key="index"></ProductCard>

            </v-row>
        </div>

    </div>

</template>

<script lang="ts">
    // @ is an alias to /src

    import { Component, Vue } from 'vue-property-decorator';
    import { Product } from '@/store/modelsData'
    import { categoryService } from "@/store/api";
    import productCard from "@/components/Cards/ProductCard.vue"


    import Categories from '../components/AdminPanel/Categories.vue';

    @Component({
        components: {
             ProductCard: productCard,

        },
    })
    export default class Home extends Vue {

        public products = new Array<Product>();
        private itemsPerRow = 3;
        async categoryProducts() {

            const catId = parseInt(this.$route.params.categoryId);
            await categoryService.categoryProducts(catId).then(res =>
                this.products = res.data as Product[]
            ).catch(ex => {
                if (process.env === 'development') {
                    console.log(ex);
                }
            }
            );

        }

        async subCategoryTree() {

            const catId = parseInt(this.$route.params.categoryId);
            const subCategories = await categoryService.subCategiories(catId);

        }


        getItem(i: number) {
            const item = this.products[i]
            return item;
        }

        async created() {
            await this.categoryProducts();
        }


        get categoryGroups() {
            const res = Array.from(Array(Math.ceil(this.products.length / this.itemsPerRow)).keys())
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
