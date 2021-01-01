
export interface SelectModel<T>{
    item: T | null;
    isSelected: boolean;
}

export interface TreeSelectModel<T> {
    item: T | null;
    isSelected: boolean;
    children: Array<TreeSelectModel<T>> | null;
}


export interface SelectItem<T> {
    Value: T;
    Text: string;
}

export interface Content {
    name: string;
    url: string;
    data: string;
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
    //token: string; 
    roles?: Array<string>;    
}

