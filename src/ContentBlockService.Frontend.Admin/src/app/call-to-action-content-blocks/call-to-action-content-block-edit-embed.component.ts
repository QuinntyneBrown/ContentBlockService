import { CallToActionContentBlock } from "./call-to-action-content-block.model";
import { EditorComponent } from "../shared";
import {  CallToActionContentBlockDelete, CallToActionContentBlockEdit, CallToActionContentBlockAdd } from "./call-to-action-content-block.actions";

const template = require("./call-to-action-content-block-edit-embed.component.html");
const styles = require("./call-to-action-content-block-edit-embed.component.scss");

export class CallToActionContentBlockEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "call-to-action-content-block",
            "call-to-action-content-block-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.callToActionContentBlock ? "Edit Call To Action Content Block": "Create Call To Action Content Block";

        if (this.callToActionContentBlock) {                
            this._nameInputElement.value = this.callToActionContentBlock.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createButtonElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createButtonElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const callToActionContentBlock = {
            id: this.callToActionContentBlock != null ? this.callToActionContentBlock.id : null,
            name: this._nameInputElement.value
        } as CallToActionContentBlock;
        
        this.dispatchEvent(new CallToActionContentBlockAdd(callToActionContentBlock));            
    }

    public onCreate() {        
        this.dispatchEvent(new CallToActionContentBlockEdit(new CallToActionContentBlock()));            
    }

    public onDelete() {        
        const callToActionContentBlock = {
            id: this.callToActionContentBlock != null ? this.callToActionContentBlock.id : null,
            name: this._nameInputElement.value
        } as CallToActionContentBlock;

        this.dispatchEvent(new CallToActionContentBlockDelete(callToActionContentBlock));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "call-to-action-content-block-id":
                this.callToActionContentBlockId = newValue;
                break;
            case "call-to-action-content-block":
                this.callToActionContentBlock = JSON.parse(newValue);
                if (this.parentNode) {
                    this.callToActionContentBlockId = this.callToActionContentBlock.id;
                    this._nameInputElement.value = this.callToActionContentBlock.name != undefined ? this.callToActionContentBlock.name : "";
                    this._titleElement.textContent = this.callToActionContentBlockId ? "Edit Call To Action Content Block" : "Create Call To Action Content Block";
                }
                break;
        }           
    }

    public callToActionContentBlockId: any;
    
	public callToActionContentBlock: CallToActionContentBlock;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".call-to-action-content-block-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".call-to-action-content-block-name") as HTMLInputElement;}
}

customElements.define(`ce-call-to-action-content-block-edit-embed`,CallToActionContentBlockEditEmbedComponent);
