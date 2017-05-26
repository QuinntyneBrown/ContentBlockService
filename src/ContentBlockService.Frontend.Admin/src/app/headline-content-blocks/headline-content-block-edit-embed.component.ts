import { HeadlineContentBlock } from "./headline-content-block.model";
import { EditorComponent } from "../shared";
import {  HeadlineContentBlockDelete, HeadlineContentBlockEdit, HeadlineContentBlockAdd } from "./headline-content-block.actions";

const template = require("./headline-content-block-edit-embed.component.html");
const styles = require("./headline-content-block-edit-embed.component.scss");

export class HeadlineContentBlockEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "headline-content-block",
            "headline-content-block-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.headlineContentBlock ? "Edit Headline Content Block": "Create Headline Content Block";

        if (this.headlineContentBlock) {                
            this._nameInputElement.value = this.headlineContentBlock.name;  
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
        const headlineContentBlock = {
            id: this.headlineContentBlock != null ? this.headlineContentBlock.id : null,
            name: this._nameInputElement.value
        } as HeadlineContentBlock;
        
        this.dispatchEvent(new HeadlineContentBlockAdd(headlineContentBlock));            
    }

    public onCreate() {        
        this.dispatchEvent(new HeadlineContentBlockEdit(new HeadlineContentBlock()));            
    }

    public onDelete() {        
        const headlineContentBlock = {
            id: this.headlineContentBlock != null ? this.headlineContentBlock.id : null,
            name: this._nameInputElement.value
        } as HeadlineContentBlock;

        this.dispatchEvent(new HeadlineContentBlockDelete(headlineContentBlock));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "headline-content-block-id":
                this.headlineContentBlockId = newValue;
                break;
            case "headline-content-block":
                this.headlineContentBlock = JSON.parse(newValue);
                if (this.parentNode) {
                    this.headlineContentBlockId = this.headlineContentBlock.id;
                    this._nameInputElement.value = this.headlineContentBlock.name != undefined ? this.headlineContentBlock.name : "";
                    this._titleElement.textContent = this.headlineContentBlockId ? "Edit HeadlineContentBlock" : "Create HeadlineContentBlock";
                }
                break;
        }           
    }

    public headlineContentBlockId: any;
    
	public headlineContentBlock: HeadlineContentBlock;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".headline-content-block-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".headline-content-block-name") as HTMLInputElement;}
}

customElements.define(`ce-headline-content-block-edit-embed`,HeadlineContentBlockEditEmbedComponent);
