<template>
    <div id="home" class="home">
        <!--<img alt="Vue logo" src="../assets/logo.png">
        <HelloWorld msg="Welcome to Your Vue.js App" />-->
        <!--<CategoryCard  :item="cat"></CategoryCard>-->

        <div v-if="categories.length > 0">
            <v-row v-for="(group, i) in categoryGroups" :key="i">

                <CategoryCard v-for="(category, index) in categories.slice(i * itemsPerRow, (i + 1) * itemsPerRow)" :item="category" :key="index"></CategoryCard>

            </v-row>
        </div>
    </div>

</template>

<script lang="ts">
    // @ is an alias to /src

    import { Component, Vue } from 'vue-property-decorator';
    import { Category } from '@/store/modelsData'
    import { categoryService } from "@/store/api";
    import categoryCard from "@/components/Cards/CategoryCard.vue"


    //import Categories from '../components/AdminPanel/Categories.vue';

    @Component({
        components: {
            CategoryCard: categoryCard
        },
    })
    export default class Home extends Vue {

        public categories = new Array<Category>();


        private itemsPerRow = 3;
        async getCategories() {
            this.categories = await categoryService.mainCategiories();
        }

        getItem(i: number) {
            const cat = this.categories[i]
            return cat as Category;
        }

        async created() {
            await this.getCategories();
        }


        get categoryGroups() {
            const res = Array.from(Array(Math.ceil(this.categories.length / this.itemsPerRow)).keys())
            return res;
        }
    }



    //export default {
    //  name: 'Home',
    //  components: {
    //    HelloWorld
    //  }
    //}
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
