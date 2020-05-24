var Ok = /** @class */ (function () {
    function Ok(value) {
        this.value = value;
    }
    Ok.prototype.isOk = function () {
        return true;
    };
    Ok.prototype.isError = function () {
        return false;
    };
    return Ok;
}());
export { Ok };
var Err = /** @class */ (function () {
    function Err(value) {
        this.value = value;
    }
    Err.prototype.isOk = function () {
        return false;
    };
    Err.prototype.isError = function () {
        return true;
    };
    return Err;
}());
export { Err };
export var ok = function (l) {
    return new Ok(l);
};
export var err = function (a) {
    return new Err(a);
};
//# sourceMappingURL=error.js.map