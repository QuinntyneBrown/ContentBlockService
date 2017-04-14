import { RestService } from "./rest-service.model";
import { EditorComponent } from "../shared";
import {  RestServiceDelete, RestServiceEdit, RestServiceAdd } from "./rest-service.actions";

const template = require("./rest-service-edit-embed.component.html");
const styles = require("./rest-service-edit-embed.component.scss");

export class RestServiceEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "rest-service",
            "rest-service-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.restService ? "Edit Rest Service": "Create Rest Service";

        if (this.restService) {                
            this._nameInputElement.value = this.restService.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const restService = {
            id: this.restService != null ? this.restService.id : null,
            name: this._nameInputElement.value
        } as RestService;
        
        this.dispatchEvent(new RestServiceAdd(restService));            
    }

    public onDelete() {        
        const restService = {
            id: this.restService != null ? this.restService.id : null,
            name: this._nameInputElement.value
        } as RestService;

        this.dispatchEvent(new RestServiceDelete(restService));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "rest-service-id":
                this.restServiceId = newValue;
                break;
            case "restService":
                this.restService = JSON.parse(newValue);
                if (this.parentNode) {
                    this.restServiceId = this.restService.id;
                    this._nameInputElement.value = this.restService.name != undefined ? this.restService.name : "";
                    this._titleElement.textContent = this.restServiceId ? "Edit RestService" : "Create RestService";
                }
                break;
        }           
    }

    public restServiceId: any;
    public restService: RestService;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".rest-service-name") as HTMLInputElement;}
}

customElements.define(`ce-rest-service-edit-embed`,RestServiceEditEmbedComponent);
