export interface Product {
    Id: Number,
    IsBestseller: Boolean,
    Name: String,
    CurrentPrice: Number,
    PreviousPrice: Number,
    Description: String,
    Image: String,
    Images: Array<String>,
    Count: Number   
}



export interface SelectItem<T> {
    Value:T,
    Text: string,
}

export interface Image {
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

