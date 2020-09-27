import { VuexModule, Module, getModule, Mutation, Action } from "vuex-module-decorators";
import store from "@/store";
import { Profile, UserLogin } from "@/store/Models";
import { Either } from '@/store/error';
import { loginUser, /*setJWT,*/ getProfile, antiforgery, logOutUser } from "../api";



enum UserRoles {
    Administrator,
    User
}


@Module({
    namespaced: true,
    name: "users",
    store,
    dynamic: true,
    preserveState: true 
})

class UsersModule extends VuexModule {
    profile: Profile | null = null; 
    isProfileLoaded = false;
    

    @Mutation setProfileEither(eitherProfile: Either<Profile | undefined, string>) {
        if (eitherProfile.isOk()) {
            this.profile = eitherProfile.value as Profile;
        }
    }
    //Mutation cannot be async 
    //if async then do Action and then mutation
    @Mutation  setProfile(profile: Profile) {

        this.profile = profile;
        this.isProfileLoaded = true;
    }

    @Mutation  logOutUser() {

        this.profile = null;
        this.isProfileLoaded = false;
    }


    get username() {
        return this.profile && this.profile.userName || null;
    }

    get isAdmin() {
        const adminRoleName = UserRoles[UserRoles.Administrator];
        let isAdmin = false;
        this.profile?.roles?.forEach((role) => {
            if (role === adminRoleName) {
                isAdmin = true;
            }
        })
        return isAdmin;
    }

    @Action({ commit: "setProfileEither" })
    async login(loginPass: UserLogin): Promise<Either<Profile | undefined, string>> {
        const userEither = await loginUser(loginPass);
        if (userEither.isOk()) {
            //const token = (userEither.value as Profile).token;
            //setJWT(token);
            await antiforgery();
        }
        return userEither;
    }

    @Action({ commit: "setProfile" })
    async getProfile(): Promise<Profile | null> {
        const user = await getProfile();
        if (user !== null) {
            //const token = user.token;
            //setJWT(token);
            await antiforgery();
        }
        return user;
    }


    @Action({ commit: "logOutUser" })
    logOut() {
         logOutUser();
        return;
    }
}
export default getModule(UsersModule);