import { __awaiter, __decorate, __extends, __generator } from "tslib";
import { VuexModule, Module, getModule, Mutation, Action } from "vuex-module-decorators";
import store from "@/store";
import { loginUser, setJWT, getProfile, antiforgery } from "../api";
var UserRoles;
(function (UserRoles) {
    UserRoles[UserRoles["Administrator"] = 0] = "Administrator";
    UserRoles[UserRoles["User"] = 1] = "User";
})(UserRoles || (UserRoles = {}));
var UsersModule = /** @class */ (function (_super) {
    __extends(UsersModule, _super);
    function UsersModule() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.profile = null;
        _this.isProfileLoaded = false;
        return _this;
    }
    UsersModule.prototype.setProfileEither = function (eitherProfile) {
        if (eitherProfile.isOk()) {
            this.profile = eitherProfile.value;
        }
    };
    UsersModule.prototype.setProfile = function (profile) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.profile = profile;
                this.isProfileLoaded = true;
                return [2 /*return*/];
            });
        });
    };
    Object.defineProperty(UsersModule.prototype, "username", {
        get: function () {
            return this.profile && this.profile.userName || null;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UsersModule.prototype, "isAdmin", {
        get: function () {
            var _a, _b;
            var adminRoleName = UserRoles[UserRoles.Administrator];
            var isAdmin = false;
            (_b = (_a = this.profile) === null || _a === void 0 ? void 0 : _a.roles) === null || _b === void 0 ? void 0 : _b.forEach(function (role) {
                if (role === adminRoleName) {
                    isAdmin = true;
                }
            });
            return isAdmin;
        },
        enumerable: false,
        configurable: true
    });
    UsersModule.prototype.login = function (loginPass) {
        return __awaiter(this, void 0, void 0, function () {
            var userEither, token;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, loginUser(loginPass)];
                    case 1:
                        userEither = _a.sent();
                        if (!userEither.isOk()) return [3 /*break*/, 3];
                        token = userEither.value.token;
                        setJWT(token);
                        return [4 /*yield*/, antiforgery()];
                    case 2:
                        _a.sent();
                        _a.label = 3;
                    case 3: return [2 /*return*/, userEither];
                }
            });
        });
    };
    UsersModule.prototype.getProfile = function () {
        return __awaiter(this, void 0, void 0, function () {
            var user, token;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, getProfile()];
                    case 1:
                        user = _a.sent();
                        if (!(user !== null)) return [3 /*break*/, 3];
                        token = user.token;
                        setJWT(token);
                        return [4 /*yield*/, antiforgery()];
                    case 2:
                        _a.sent();
                        _a.label = 3;
                    case 3: return [2 /*return*/, user];
                }
            });
        });
    };
    __decorate([
        Mutation
    ], UsersModule.prototype, "setProfileEither", null);
    __decorate([
        Mutation
    ], UsersModule.prototype, "setProfile", null);
    __decorate([
        Action({ commit: "setProfileEither" })
    ], UsersModule.prototype, "login", null);
    __decorate([
        Action({ commit: "setProfile" })
    ], UsersModule.prototype, "getProfile", null);
    UsersModule = __decorate([
        Module({
            namespaced: true,
            name: "users",
            store: store,
            dynamic: true
        })
    ], UsersModule);
    return UsersModule;
}(VuexModule));
export default getModule(UsersModule);
//# sourceMappingURL=Users.js.map