import { VuexModule, Module, getModule, Mutation, Action } from "vuex-module-decorators";
import { loadCount, addProductToCard, removeProductFromCard} from "../api";
import { Product } from "../modelsData";
import store from "@/store";


@Module({
    namespaced: true,
    name: "cart",
    store,
    dynamic: true,
    preserveState: true
})

class CartModule extends VuexModule {

    cartCount: number | null = null;
    @Mutation setCartCount(cartCount: number | undefined) {
        if (typeof cartCount !== 'undefined' && cartCount != null) {
            this.cartCount = cartCount;
        }

    }

    @Action({
        commit: "setCartCount",
        rawError: true
    })
    async loadCartCount(): Promise<number | null> {
        const count = await loadCount();
        return count;
    }

    @Action({
        commit: "setCartCount",
        rawError: true
    })
    async addToCard(product: Product): Promise<number | undefined> {
        const count = await addProductToCard(product);
        return count;
    }

    @Action({
        commit: "setCartCount",
        rawError: true
    })
    async removeFromCard(product: Product): Promise<number | undefined> {
        const count = await removeProductFromCard(product);
        return count;
    }
}
export default getModule(CartModule);
