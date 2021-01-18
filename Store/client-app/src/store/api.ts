import axios from 'axios'
import { UserLogin, Profile } from '@/store/Models';
import { Category, DataTableSearchViewModel, DeliveryMethod, Product, RegisterModel } from '@/store/ModelsData';
import { ok, err, Either } from '@/store/error';
import antiforgeryState from "@/store/Modules/Antiforgery";
const head = antiforgeryState?.antiforgeryToken ?? "";

export const api = axios.create({
    baseURL: "/api/",
    headers: { 'X-XSRF-TOKEN': head }
});

class ApiBase<T> {
    protected controller: string;

    constructor(controller: string) {
        this.controller = controller;
    }

    async get(id: number) {
        const res = await api.get(`${this.controller}/${id}`).then(
            (r) => {
                return r;
            });
        return res.data as T;
    }

    async list() {
        const res = await api.get(`${this.controller}`).then(
            (r) => {
                return r;
            });
        return res.data as T[];
    }

    async search(page: number, rowsPerPage: number, query: string | null = null) {
        const res = await api.get(`${this.controller}/search?page=${page}&pageSize=${rowsPerPage}&query=${query}&sort=SortOrder|asc'`).then(
            (r) => {
                return r;
            });
        return res.data as DataTableSearchViewModel<T>;
    }

    async create(params: T) {
        return await api.post(`${this.controller}`, params);
    }

    async update(params: T) {
        return await api.put(`${this.controller}`, params);
    }

    async destroy(id: number) {
        return await api.delete(`${this.controller}/${id}`);
    }

}

//now jwt stored in httponly cookie
//export function setJWT(token: string) {
//    api.defaults.headers.common['Authorization'] = `Bearer ${token}`
//}

//export function clearJWT() {
//    delete api.defaults.headers.common['Authorization']
//}

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

export function getCookie(name: string) {
    const value = "; " + document.cookie;
    const parts = value.split("; " + name + "=");

    if (parts.length == 2) {
        const part = parts.pop();
        if (part) {
            return part.split(";").shift();
        }
    }
}

export async function antiforgery(): Promise<string | undefined> {
    const res = await api.get('/antiforgery').then(
        (r) => {
            console.log(r.headers["set-cookies"]);
            const antiforgeryToken = getCookie("XSRF-REQUEST-TOKEN");
            api.defaults.headers.common['X-XSRF-TOKEN'] = antiforgeryToken;
            return antiforgeryToken;
        });

    return res;
}

export async function registerUser(userData: RegisterModel) {
    return await api.post('/user/register', userData)
}

export async function loadCount(): Promise<number | null> {

    const resp = await api.get('/shoppingcart/count').then(
        (r) => {
            return r;
        }
    ).catch((e) => {
        console.log(e);
        return e.response;
    });
    if (resp.status === 200) {
        return resp.data as number;
    }

    else return 0;
}
export async function addProductToCard(product: Product): Promise<number | undefined> {

    let count = 0;
    const resp = await api.post(`/shoppingcart/${product.id}`).then((r) => {
        return r;
    }
    ).catch((e) => {
        console.log(e);
        return e.response;
    });
    if (resp.status === 200) {
        count = (resp.data as number);
    }

    return count;
}
export async function removeProductFromCard(product: Product): Promise<number | undefined> {

    let count = 0;
    const resp = await api.delete(`/shoppingcart/${product.id}`)
        .then((r) => {
        return r;
    }
    ).catch((e) => {
        console.log(e);
        return e.response;
    });
    if (resp.status === 200) {
        count = (resp.data as number);
    }

    return count;
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

export async function logOutUser() {
    await api.get('/user/logOff')
    return;
}


//export async function Products(categoryId: number, page: number, pageSize: number) {
//    const resp = await api.get("category/subcategories/${parentCategoryId}")
//    return resp;
//}

class CategoryService extends ApiBase<Category>{
    async tree() {
        const res = await api.get(`${this.controller}/tree`).then(
            (r) => {
                return r;
            });
        return res.data as Category[];
    }

    async mainCategiories() {
        const resp = await api.get(`${this.controller}/main`)
        return resp.data as Category[];
    }

    async subCategiories(id: number) {
        const resp = await api.get(`${this.controller}/${id}/subcategories`)
        return resp;
    }

    async categoryProducts(id: number) {

        const resp = await api.get(`${this.controller}/${id}/categoryProducts`)
        return resp;
    }
}

class ProductService extends ApiBase<Product>{




}





export const categoryService = new CategoryService("categories");

export const productService = new ProductService("products")

export const deliveryMehodService = new ApiBase<DeliveryMethod>("deliverymethods");





