import { RouterOutlet } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "content-block-master-detail", authRequired: true },
            { path: "/tab/:tabIndex", name: "content-block-master-detail", authRequired: true },
            { path: "/content-block/edit/:contentBlockId/tab/:tabIndex", name: "content-block-master-detail", authRequired: true },
            { path: "/content-block/edit/:contentBlockId", name: "content-block-master-detail", authRequired: true },

            { path: "/headline-content-blocks", name: "headline-content-block-master-detail", authRequired: true },

            { path: "/call-to-action-content-blocks", name: "call-to-action-content-block-master-detail", authRequired: true },

            { path: "/register", name: "account-register" },
            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }
        ] as any);

        this.use(new AuthorizedRouteMiddleware());

        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);