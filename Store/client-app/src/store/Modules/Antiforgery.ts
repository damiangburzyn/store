import { VuexModule, Module, getModule, Mutation, Action } from "vuex-module-decorators";
import { antiforgery } from "../api";
import store from "@/store";


@Module({
    namespaced: true,
    name: "antiforgery",
    store,
    dynamic: true,
    preserveState: true
})

class AntiforgeryModule extends VuexModule {

    antiforgeryToken: string | null = null;
    @Mutation setAntiforgery(antiforgery: string | undefined) {
        if (typeof antiforgery !== 'undefined' && antiforgery != null) {
            this.antiforgeryToken = antiforgery;
        }

    }

    @Action({
        commit: "setAntiforgery",
         rawError: true 
    })
    async loadAntiforgery(): Promise<string | undefined> {
        const token = await antiforgery();
        return token;
    }

}
export default getModule(AntiforgeryModule);
