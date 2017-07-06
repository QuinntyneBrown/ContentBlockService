import { MegaHeaderContentBlock } from "./mega-header-content-block.model";
import { EditorComponent } from "../shared";
import {  MegaHeaderContentBlockDelete, MegaHeaderContentBlockEdit, MegaHeaderContentBlockAdd } from "./mega-header-content-block.actions";

const template = require("./mega-header-content-block-edit-embed.component.html");
const styles = require("./mega-header-content-block-edit-embed.component.css");

export class MegaHeaderContentBlockEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "mega-header-content-block",
            "mega-header-content-block-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.megaHeaderContentBlock ? "Edit Mega Header Content Block": "Create Mega Header Content Block";

        if (this.megaHeaderContentBlock) {                
            this._nameInputElement.value = this.megaHeaderContentBlock.name;  
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
        const megaHeaderContentBlock = {
            id: this.megaHeaderContentBlock != null ? this.megaHeaderContentBlock.id : null,
            name: this._nameInputElement.value
        } as MegaHeaderContentBlock;
        
        this.dispatchEvent(new MegaHeaderContentBlockAdd(megaHeaderContentBlock));            
    }

    public onCreate() {        
        this.dispatchEvent(new MegaHeaderContentBlockEdit(new MegaHeaderContentBlock()));            
    }

    public onDelete() {        
        const megaHeaderContentBlock = {
            id: this.megaHeaderContentBlock != null ? this.megaHeaderContentBlock.id : null,
            name: this._nameInputElement.value
        } as MegaHeaderContentBlock;

        this.dispatchEvent(new MegaHeaderContentBlockDelete(megaHeaderContentBlock));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "mega-header-content-block-id":
                this.megaHeaderContentBlockId = newValue;
                break;
            case "mega-header-content-block":
                this.megaHeaderContentBlock = JSON.parse(newValue);
                if (this.parentNode) {
                    this.megaHeaderContentBlockId = this.megaHeaderContentBlock.id;
                    this._nameInputElement.value = this.megaHeaderContentBlock.name != undefined ? this.megaHeaderContentBlock.name : "";
                    this._titleElement.textContent = this.megaHeaderContentBlockId ? "Edit MegaHeaderContentBlock" : "Create MegaHeaderContentBlock";
                }
                break;
        }           
    }

    public megaHeaderContentBlockId: any;
    
	public megaHeaderContentBlock: MegaHeaderContentBlock;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".mega-header-content-block-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".mega-header-content-block-name") as HTMLInputElement;}
}

customElements.define(`ce-mega-header-content-block-edit-embed`,MegaHeaderContentBlockEditEmbedComponent);
