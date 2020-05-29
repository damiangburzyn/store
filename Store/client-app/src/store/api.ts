import axios from 'axios'
import { UserLogin, Profile } from '@/store/Models';
import {Category } from '@/store/ModelsData';
import { ok, err, Either } from '@/store/error';


export const api = axios.create({
    baseURL: "/api/"
});

export function setJWT(token: string) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`
}

export function clearJWT() {
    delete api.defaults.headers.common['Authorization']
}


export async function getProfile(): Promise<Profile | null> {
    try {

        const resp = await api.get('/user/getProfile').then(
            (r) => {
                return r;
            }
        ).catch((e) => {
            console.log(e);
            return e.response;
        });
        if (resp.status === 200) {
            return resp.data as Profile;
        }

        else return null;
    }
    catch (error) {
        console.log("wystąpił błąd ", error)
        return null;
    }
}



export async function loginUser(userSubmit: UserLogin): Promise<Either<Profile | undefined, string>> {
    try {

        const resp = await api.post('/user/login', userSubmit).then(
            (r) => {
                return r;
            }
        ).catch((e) => {
            console.log(e);
            return e.response;
        });
        if (resp.status === 200) {
            return ok(resp.data as Profile);
        }
        else
            console.log(resp); {
            if (typeof (resp.data) === "string") {
                const errorMsg = resp.data as string;
                return err(errorMsg);
            }
            else {
                console.log("wystąpił błąd ", resp)
                return err("Nieprawidłowe login lub hasło");
            }
        }
    }
    catch (error) {
        console.log("wystąpił błąd ", error)
        return err("Nieprawidłowe login lub hasło");
    }
}



export async function Products(categoryId: number, page: number, pageSize: number) {
    const resp = await api.get("category/subcategories/${parentCategoryId}")
    return resp;
}


export const CategoryService = {
     controller : "categories",

    async Get(id: number) {
        const res = await api.get(`${this.controller}/${id}`).then(
            (r) => {
                return r;
            });
        return res.data as Category;
    },

    async  Tree() {
        const res = await api.get(`${this.controller}/tree`).then(
            (r) => {
                return r;
            });
        return res.data as Category[];      
    },


    async  MainCategiories() {
        const resp = await api.get(`${this.controller}/main`)
        return resp;
    },

    async  SubCategiories(parentCategoryId: number) {
        const resp = await api.get(`${this.controller}/subcategories/${parentCategoryId}`)
        return resp;
    },

    async create(params: Category) {
        return await api.post(`${this.controller}`, params );
    },

    async update(params: Category) {
        return await api.put(`${this.controller}`,params );
    },

    async destroy(id: number) {
        return await api.delete(`${this.controller}/${id}`);
    }
}
