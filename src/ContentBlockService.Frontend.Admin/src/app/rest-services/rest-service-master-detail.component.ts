import { RestServiceAdd, RestServiceDelete, RestServiceEdit, restServiceActions } from "./rest-service.actions";
import { RestService } from "./rest-service.model";
import { RestServiceService } from "./rest-service.service";

const template = require("./rest-service-master-detail.component.html");
const styles = require("./rest-service-master-detail.component.scss");

export class RestServiceMasterDetailComponent extends HTMLElement {
    constructor(
        private _restServiceService: RestServiceService = RestServiceService.Instance	
	) {
        super();
        this.onRestServiceAdd = this.onRestServiceAdd.bind(this);
        this.onRestServiceEdit = this.onRestServiceEdit.bind(this);
        this.onRestServiceDelete = this.onRestServiceDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "rest-services"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.restServices = await this._restServiceService.get();
        this.restServiceListElement.setAttribute("rest-services", JSON.stringify(this.restServices));
    }

    private _setEventListeners() {
        this.addEventListener(restServiceActions.ADD, this.onRestServiceAdd);
        this.addEventListener(restServiceActions.EDIT, this.onRestServiceEdit);
        this.addEventListener(restServiceActions.DELETE, this.onRestServiceDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(restServiceActions.ADD, this.onRestServiceAdd);
        this.removeEventListener(restServiceActions.EDIT, this.onRestServiceEdit);
        this.removeEventListener(restServiceActions.DELETE, this.onRestServiceDelete);
    }

    public async onRestServiceAdd(e) {

        await this._restServiceService.add(e.detail.restService);
        this.restServices = await this._restServiceService.get();
        
        this.restServiceListElement.setAttribute("rest-services", JSON.stringify(this.restServices));
        this.restServiceEditElement.setAttribute("rest-service", JSON.stringify(new RestService()));
    }

    public onRestServiceEdit(e) {
        this.restServiceEditElement.setAttribute("rest-service", JSON.stringify(e.detail.restService));
    }

    public async onRestServiceDelete(e) {

        await this._restServiceService.remove(e.detail.restService.id);
        this.restServices = await this._restServiceService.get();
        
        this.restServiceListElement.setAttribute("rest-services", JSON.stringify(this.restServices));
        this.restServiceEditElement.setAttribute("rest-service", JSON.stringify(new RestService()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "rest-services":
                this.restServices = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<RestService> { return this.restServices; }

    private restServices: Array<RestService> = [];
    public restService: RestService = <RestService>{};
    public get restServiceEditElement(): HTMLElement { return this.querySelector("ce-rest-service-edit-embed") as HTMLElement; }
    public get restServiceListElement(): HTMLElement { return this.querySelector("ce-rest-service-list-embed") as HTMLElement; }
}

customElements.define(`ce-rest-service-master-detail`,RestServiceMasterDetailComponent);
