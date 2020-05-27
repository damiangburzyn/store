import { __awaiter, __generator } from "tslib";
import axios from 'axios';
import { ok, err } from '@/store/error';
export var api = axios.create({
    baseURL: "/api/"
});
export function setJWT(token) {
    api.defaults.headers.common['Authorization'] = "Bearer " + token;
}
export function clearJWT() {
    delete api.defaults.headers.common['Authorization'];
}
export function getProfile() {
    return __awaiter(this, void 0, void 0, function () {
        var resp, error_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    return [4 /*yield*/, api.get('/user/getProfile').then(function (r) {
                            return r;
                        }).catch(function (e) {
                            console.log(e);
                            return e.response;
                        })];
                case 1:
                    resp = _a.sent();
                    if (resp.status === 200) {
                        return [2 /*return*/, resp.data];
                    }
                    else
                        return [2 /*return*/, null];
                    return [3 /*break*/, 3];
                case 2:
                    error_1 = _a.sent();
                    console.log("wystąpił błąd ", error_1);
                    return [2 /*return*/, null];
                case 3: return [2 /*return*/];
            }
        });
    });
}
export function loginUser(userSubmit) {
    return __awaiter(this, void 0, void 0, function () {
        var resp, errorMsg, error_2;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    return [4 /*yield*/, api.post('/user/login', userSubmit).then(function (r) {
                            return r;
                        }).catch(function (e) {
                            console.log(e);
                            return e.response;
                        })];
                case 1:
                    resp = _a.sent();
                    if (resp.status === 200) {
                        return [2 /*return*/, ok(resp.data)];
                    }
                    else
                        console.log(resp);
                    {
                        if (typeof (resp.data) === "string") {
                            errorMsg = resp.data;
                            return [2 /*return*/, err(errorMsg)];
                        }
                        else {
                            console.log("wystąpił błąd ", resp);
                            return [2 /*return*/, err("Nieprawidłowe login lub hasło")];
                        }
                    }
                    return [3 /*break*/, 3];
                case 2:
                    error_2 = _a.sent();
                    console.log("wystąpił błąd ", error_2);
                    return [2 /*return*/, err("Nieprawidłowe login lub hasło")];
                case 3: return [2 /*return*/];
            }
        });
    });
}
export function Products(categoryId, page, pageSize) {
    return __awaiter(this, void 0, void 0, function () {
        var resp;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, api.get("category/subcategories/${parentCategoryId}")];
                case 1:
                    resp = _a.sent();
                    return [2 /*return*/, resp];
            }
        });
    });
}
export var CategoryService = {
    controller: "categories",
    Get: function (id) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, api.get(this.controller + "/" + id)];
            });
        });
    },
    MainCategiories: function () {
        return __awaiter(this, void 0, void 0, function () {
            var resp;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, api.get(this.controller + "/main")];
                    case 1:
                        resp = _a.sent();
                        return [2 /*return*/, resp];
                }
            });
        });
    },
    SubCategiories: function (parentCategoryId) {
        return __awaiter(this, void 0, void 0, function () {
            var resp;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, api.get(this.controller + "/subcategories/" + parentCategoryId)];
                    case 1:
                        resp = _a.sent();
                        return [2 /*return*/, resp];
                }
            });
        });
    },
    create: function (params) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, api.post("" + this.controller, params)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    },
    update: function (params) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, api.put("" + this.controller, params)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    },
    destroy: function (id) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, api.delete(this.controller + "/" + id)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    }
};
//# sourceMappingURL=api.js.map