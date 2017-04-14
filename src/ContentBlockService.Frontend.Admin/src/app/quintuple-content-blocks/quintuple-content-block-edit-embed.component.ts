import { QuintupleContentBlock } from "./quintuple-content-block.model";
import { EditorComponent } from "../shared";
import {  QuintupleContentBlockDelete, QuintupleContentBlockEdit, QuintupleContentBlockAdd } from "./quintuple-content-block.actions";

const template = require("./quintuple-content-block-edit-embed.component.html");
const styles = require("./quintuple-content-block-edit-embed.component.scss");

export class QuintupleContentBlockEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "quintuple-content-block",
            "quintuple-content-block-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.quintupleContentBlock ? "Edit Quintuple Content Block": "Create Quintuple Content Block";

        if (this.quintupleContentBlock) {                
            this._nameInputElement.value = this.quintupleContentBlock.name;  
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
        const quintupleContentBlock = {
            id: this.quintupleContentBlock != null ? this.quintupleContentBlock.id : null,
            name: this._nameInputElement.value
        } as QuintupleContentBlock;
        
        this.dispatchEvent(new QuintupleContentBlockAdd(quintupleContentBlock));            
    }

    public onDelete() {        
        const quintupleContentBlock = {
            id: this.quintupleContentBlock != null ? this.quintupleContentBlock.id : null,
            name: this._nameInputElement.value
        } as QuintupleContentBlock;

        this.dispatchEvent(new QuintupleContentBlockDelete(quintupleContentBlock));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "quintuple-content-block-id":
                this.quintupleContentBlockId = newValue;
                break;
            case "quintupleContentBlock":
                this.quintupleContentBlock = JSON.parse(newValue);
                if (this.parentNode) {
                    this.quintupleContentBlockId = this.quintupleContentBlock.id;
                    this._nameInputElement.value = this.quintupleContentBlock.name != undefined ? this.quintupleContentBlock.name : "";
                    this._titleElement.textContent = this.quintupleContentBlockId ? "Edit QuintupleContentBlock" : "Create QuintupleContentBlock";
                }
                break;
        }           
    }

    public quintupleContentBlockId: any;
    public quintupleContentBlock: QuintupleContentBlock;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".quintuple-content-block-name") as HTMLInputElement;}
}

customElements.define(`ce-quintuple-content-block-edit-embed`,QuintupleContentBlockEditEmbedComponent);
