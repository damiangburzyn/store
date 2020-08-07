



export interface SelectItem<T> {
    Value:T,
    Text: string,
}

export interface Content {
    name: string,
    url: string,
    data: string,
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

