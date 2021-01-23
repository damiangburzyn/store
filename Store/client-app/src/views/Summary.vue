<template>
    <div>

        <v-row no-gutters>
            <v-col md="8"
                   offset-md="2">

                <h3>Podsumowanie</h3>
                <br />
                <v-card class="pa-2"
                        outlined
                        tile>
                    <v-simple-table dense>
                        <template v-slot:default>
                            <thead>
                                <tr>
                                    <th class="text-left">
                                        Produkt
                                    </th>
                                    <th class="text-left">
                                        Ilośc sztuk
                                    </th>
                                    <th class="text-left">
                                        cena za sztukę
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in shoppingCart.cartItems"
                                    :key="item.name">
                                    <td>{{ item.product.name }}</td>
                                    <td>
                                        <input type="number"
                                               v-model="item.count"
                                               min="0">
                                    </td>
                                    <td>{{ item.product.currentPrice }}</td>
                                </tr>
                            </tbody>
                        </template>
                    </v-simple-table>

                </v-card>
            </v-col>
        </v-row>
        <br />
        <v-row no-gutters
               style="height: 150px;">
            <v-col outlined
                   offset="8"
                   md="2">

                Suma {{ shoppingCart.cartTotal}} zł

            </v-col>
        </v-row>









        <v-row no-gutters>
            <v-col md="8"
                   offset-md="2">

                <h3>Opcje dostawy</h3>
                <br />
                <v-card class="pa-2"
                        outlined
                        tile>
                    <v-simple-table dense>
                        <template v-slot:default>
                            <thead>
                                <tr>
                                    <th class="text-left">
                                        Wybierz
                                    </th>

                                    <th class="text-left">
                                        Typ dostawy
                                    </th>
                                    <th class="text-left">
                                        Opis
                                    </th>
                                    <th class="text-left">
                                        cena
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in deliveryMethods"
                                    :key="item.name">
                                    <td>
                                        <input type="radio" id="item.name" v-bind:value="item.name" v-model="methodName">
                                       


                                    </td>
                                    <td>{{ item.name }}</td>
                                    <td>
                                        {{ item.description }}
                                    </td>
                                    <td>{{calculatePrice(item)}}</td>
                                </tr>
                            </tbody>
                        </template>
                    </v-simple-table>

                </v-card>
            </v-col>
        </v-row>
        <br />
        <v-row no-gutters
               style="height: 150px;">
            <v-col outlined
                   offset="8"
                   md="2">

                Łącznie {{ shoppingCart.cartTotal}} zł

            </v-col>
        </v-row>





    </div>
</template>


<script lang="ts">
    import { Vue, Component } from "vue-property-decorator";
    import { cartService, deliveryMehodService } from "@/store/api";
    import { DeliveryMethod, ShoppingCart } from '@/store/modelsData'
    @Component
    export default class Register extends Vue {

        shoppingCart = new ShoppingCart();
        deliveryMethods = new Array<DeliveryMethod>();
        selectedDeliveryMethod: DeliveryMethod|null = null;
        methodName= "";

        calculatePrice(item: DeliveryMethod) {
            return 0;
        }

        async loadShoppingCart() {
            await cartService.list().then(res => {
                this.shoppingCart = res;
            }
            ).catch(ex => {
                this.shoppingCart = new ShoppingCart();
                if (process.env !== "production") {
                    console.log(ex)
                }
            })}

        async loadDeliveryMethods() {
            await deliveryMehodService.list().then(res => {
                this.deliveryMethods = res;
            }
            ).catch(ex => {
                this.deliveryMethods = [];
                if (process.env !== "production") {
                    console.log(ex)
                }
            })
        }
         
        async created() {
            await Promise.all([this.loadShoppingCart(), this.loadDeliveryMethods()]);
        }

    

    }



</script>