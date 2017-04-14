import { ContentBlock } from "./content-block.model";
import { EditorComponent } from "../shared";
import {  ContentBlockDelete, ContentBlockEdit, ContentBlockAdd } from "./content-block.actions";

const template = require("./content-block-edit-embed.component.html");
const styles = require("./content-block-edit-embed.component.scss");

export class ContentBlockEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "content-block",
            "content-block-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.contentBlock ? "Edit Content Block": "Create Content Block";

        if (this.contentBlock) {                
            this._nameInputElement.value = this.contentBlock.name;  
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
        const contentBlock = {
            id: this.contentBlock != null ? this.contentBlock.id : null,
            name: this._nameInputElement.value
        } as ContentBlock;
        
        this.dispatchEvent(new ContentBlockAdd(contentBlock));            
    }

    public onDelete() {        
        const contentBlock = {
            id: this.contentBlock != null ? this.contentBlock.id : null,
            name: this._nameInputElement.value
        } as ContentBlock;

        this.dispatchEvent(new ContentBlockDelete(contentBlock));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "content-block-id":
                this.contentBlockId = newValue;
                break;
            case "contentBlock":
                this.contentBlock = JSON.parse(newValue);
                if (this.parentNode) {
                    this.contentBlockId = this.contentBlock.id;
                    this._nameInputElement.value = this.contentBlock.name != undefined ? this.contentBlock.name : "";
                    this._titleElement.textContent = this.contentBlockId ? "Edit ContentBlock" : "Create ContentBlock";
                }
                break;
        }           
    }

    public contentBlockId: any;
    public contentBlock: ContentBlock;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".content-block-name") as HTMLInputElement;}
}

customElements.define(`ce-content-block-edit-embed`,ContentBlockEditEmbedComponent);
