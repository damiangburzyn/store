import { Content } from '@/store/models';

export class Category {
    id: number;
    sortOrder: number | undefined;
    name: string;
    shortName: string;
    logo: Content;
    parentCategoryId: number | null | undefined;
    subCategories: Array<Category | undefined>

    constructor() {
        this.id = 0;
        this.sortOrder = 0;
        this.name = '';
        this.shortName = "";
        this.parentCategoryId = null;
        this.logo = { data: "", name: "", url: "" };
        this.subCategories = [];
    }
}

export class DeliveryMethod {
    id: number;
    name: string;
    description: string;
    constructor() {
        this.id = 0;
        this.name = '';
        this.description = '';
    }

}

export class ProductCategory {
    id: number;   
    categoryId: number;
    productId: number;
    category: Category | null;
    product: Product | null;
    constructor() {
        this.id = 0;
        this.productId = 0;
        this.categoryId = 0;
        this.category = null;
        this.product = null;
    }
}

export class Product {
    id: number ;   
    isBestseller: boolean;
    name: string;
    currentPrice: number;
    previousPrice: number;
    description: string;
    images: Array<Content> = [];
    movies: Array<Content> = [];
   // deliveryMethods: Array<ProductDeliveryMethod> = [];
    productCategories: Array<ProductCategory> = [];
    constructor() {
        this.id = 0;
        this.isBestseller = false;
        this.name = '';
        this.currentPrice = 0.00;
        this.previousPrice = 0.00;
        this.description = '';
    }
}

export class ProductDeliveryMethod {
    id: number;
    deliveryId: number;
    productId: number;
    maxCountInPackage: number;
    price: number;
    delivery: DeliveryMethod;
    constructor() {
        this.id = 0;
        this.deliveryId = 0;
        this.productId = 0;
        this.maxCountInPackage = 1
        this.price = 0.00;
        this.delivery = new DeliveryMethod();
    }
}

export class DataTableSearchViewModel<T>{
    total: number;
    perPage: number;
    currentPage: number;
    lastPage: number;
    from: number;
    to: number;
    nextPageUrl: string;
    prevPageUrl: string;
    data: Array<T>;

    constructor() {
        this.total = 0;
        this.perPage = 0;
        this.currentPage = 0;
        this.lastPage = 0;
        this.from = 0;
        this.to = 0;
        this.nextPageUrl = '';
        this.prevPageUrl = '';
        this.data = [];
    }
}

export class RegisterModel {

    password: string;
    repeatPassword: string;
    email: string;
    acceptTermsOfUse: boolean;
    name: string;
    lastname: string;
    phone: string;

    constructor() {
        this.password = '';
        this.repeatPassword = '';
        this.email = '';
        this.acceptTermsOfUse = false;
        this.name = '';
        this.lastname = '';
        this.phone = '';
    }
}

export class ShoppingCart {
    cartItems: CartModel[];
    cartTotal: number;
    constructor() {
        this.cartItems = [];
        this.cartTotal = 0;
    }
}

export class CartModel {

    cartId: string;
    productId: number;
    count: number;
    product: Product;

    constructor() {
        this.cartId = '';
        this.productId = 0;
        this.count = 0;
        this.product = new Product();
    }
}