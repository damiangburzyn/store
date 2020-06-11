export interface Product {
    id: Number,
    isBestseller: Boolean,
    name: String,
    currentPrice: Number,
    previousPrice: Number,
    description: String,
    count: Number   
    images: Array<Content>,
    movies: Array<Content>,

}



export interface SelectItem<T> {
    Value:T,
    Text: string,
}

export interface Content {
    name: string,
    url: string,
    data: string,
}


export interface ProductEdit extends Product {
    ImagesBuffer: Array<String>; 
} 

export interface UserLogin {
    email: string;
    password: string;
}


export interface Profile {
    id: number;
    firstName?: string  ;
    lastName?: string ;
    userName: string;
    token: string; 
    roles?: Array<string>;    
}

