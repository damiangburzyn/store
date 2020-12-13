import { __awaiter, __decorate, __extends, __generator } from "tslib";
import { VuexModule, Module, getModule, Mutation, Action } from "vuex-module-decorators";
import { antiforgery } from "../api";
import store from "@/store";
var AntiforgeryModule = /** @class */ (function (_super) {
    __extends(AntiforgeryModule, _super);
    function AntiforgeryModule() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.antiforgeryToken = null;
        return _this;
    }
    AntiforgeryModule.prototype.setAntiforgery = function (antiforgery) {
        if (typeof antiforgery !== 'undefined' && antiforgery != null) {
            this.antiforgeryToken = antiforgery;
        }
    };
    AntiforgeryModule.prototype.loadAntiforgery = function () {
        return __awaiter(this, void 0, void 0, function () {
            var token;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, antiforgery()];
                    case 1:
                        token = _a.sent();
                        return [2 /*return*/, token];
                }
            });
        });
    };
    __decorate([
        Mutation
    ], AntiforgeryModule.prototype, "setAntiforgery", null);
    __decorate([
        Action({
            commit: "setAntiforgery",
            rawError: true
        })
    ], AntiforgeryModule.prototype, "loadAntiforgery", null);
    AntiforgeryModule = __decorate([
        Module({
            namespaced: true,
            name: "antiforgery",
            store: store,
            dynamic: true,
            preserveState: true
        })
    ], AntiforgeryModule);
    return AntiforgeryModule;
}(VuexModule));
export default getModule(AntiforgeryModule);
//# sourceMappingURL=Antiforgery.js.map