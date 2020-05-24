export type LoginResult<T, E> = Ok<T, E> // contains a success value of type T
    | Err<T, E> // contains a failure value of type E


export type Either<L, A> = Ok<L, A> | Err<L, A>;

export class Ok<L, A> {
    readonly value: L;

    constructor(value: L) {
        this.value = value;
    }

    isOk(): this is Ok<L, A> {
        return true;
    }

    isError(): this is Err<L, A> {
        return false;
    }
}

export class Err<L, A> {
    readonly value: A;

    constructor(value: A) {
        this.value = value;
    }

    isOk(): this is Ok<L, A> {
        return false;
    }

    isError(): this is Err<L, A> {
        return true;
    }
}


export const ok = <L, A>(l: L): Either<L, A> => {
    return new Ok(l);
};

export const err = <L, A>(a: A): Either<L, A> => {
    return new Err<L, A>(a);
};