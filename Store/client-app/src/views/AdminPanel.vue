<template>
    <v-container>

        <v-row>
            <v-col sm="2">
                <v-card class="mx-auto"
                        max-width="300"
                        tile>
                    <v-list shaped>
                        <v-subheader>Panel</v-subheader>
                        <v-list-item-group v-model="selectedTabIndex" color="primary">
                            <v-list-item v-for="(item, i) in items"
                                         :key="i"  v-on:click="activate(item)">
                                <v-list-item-icon>
                                    <v-icon v-text="item.icon"></v-icon>
                                </v-list-item-icon>
                                <v-list-item-content>
                                    <v-list-item-title v-text="item.title"></v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                        </v-list-item-group>
                    </v-list>
                </v-card>
            </v-col>

            <v-col sm="10">
                <v-card>
                    <!--color="#b3d4fc"-->
                    <component v-bind:is="dynamicComponent"></component>
                </v-card>
            </v-col>
        </v-row>


        


    </v-container>
</template>
<script lang="ts">
import {Vue, Component} from "vue-property-decorator";
//import UserLogin from "@/store/models";
//import users from "@/store/Modules/Users";
//import { VueClass } from 'vue-class-component/lib/declarations';

    import  Products  from '@/components/AdminPanel/Products.vue';
    import Categories from '@/components/AdminPanel/Categories.vue';
    import  DeliveryOptions from '@/components/AdminPanel/DeliveryOptions.vue';


    interface PanelItem {
        title: string;
        icon: string;
        isActive: boolean;
        component: string;
    }


    @Component({
        components : {
            Products: Products,
            Categories: Categories,
            DeliveryOptions: DeliveryOptions
        },})
export default class AdminPanel extends Vue {
    

    selectedTabIndex = 0;
    items: Array<PanelItem>=[
        { title: 'Kategorie', icon: 'mdi-view-dashboard', isActive: true , component: 'Categories' },
        { title: 'Produkty', icon: 'mdi-image', isActive: false, component: 'Products' },
        { title: 'Opcje dostawy', icon: 'mdi-help-box', isActive: false, component:  'DeliveryOptions'},
    ];
    color = 'blue';
    colors = [
        'primary',
        'blue',
        'success',
        'red',
        'teal',
    ];
   
    get dynamicComponent() {

        let component = 'Categories';
        
        this.items.forEach(item => {

            if (item.isActive) {
                component = item.component;
            }

        });
        return component;
    }

    activate(panelItem: PanelItem) {
        this.items.forEach(item => {
            item.isActive = false;
        });
     
        panelItem.isActive = true;

    }
}
</script>