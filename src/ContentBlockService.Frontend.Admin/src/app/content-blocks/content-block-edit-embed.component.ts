import { ContentBlock } from "./content-block.model";
import { EditorComponent } from "../shared";
import { ContentBlockDelete, ContentBlockEdit, ContentBlockAdd } from "./content-block.actions";

const template = require("./content-block-edit-embed.component.html");
const styles = require("./content-block-edit-embed.component.scss");

export class ContentBlockEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
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
        this._titleElement.textContent = this.contentBlock ? `Edit Content Block: ${this.contentBlock.name}` : "Create Content Block";

        this.htmlContentEditor = new EditorComponent(this._htmlContentInputElement);

        if (this.contentBlock) {                
            this._nameInputElement.value = this.contentBlock.name;  
            this._urlInputElement.value = this.contentBlock.url;
            this._imageUrlInputElement.value = this.contentBlock.imageUrl;
            this._heading1InputElement.value = this.contentBlock.heading1;
            this._heading2InputElement.value = this.contentBlock.heading2;
            this.htmlContentEditor.setHTML(this.contentBlock.htmlContent);
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
        const contentBlock = {
            id: this.contentBlock != null ? this.contentBlock.id : null,
            name: this._nameInputElement.value,
            url: this._urlInputElement.value,
            imageUrl: this._imageUrlInputElement.value,
            heading1: this._heading1InputElement.value,
            heading2: this._heading2InputElement.value,
            htmlContent: this.htmlContentEditor.text           
        } as ContentBlock;
        
        this.dispatchEvent(new ContentBlockAdd(contentBlock));            
    }

    public onCreate() {        
        this.dispatchEvent(new ContentBlockEdit(new ContentBlock()));            
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
            case "content-block":
                this.contentBlock = JSON.parse(newValue);
                if (this.parentNode) {
                    this.contentBlockId = this.contentBlock.id;
                    this._nameInputElement.value = this.contentBlock.name != undefined ? this.contentBlock.name : "";
                    this._urlInputElement.value = this.contentBlock.url != undefined ? this.contentBlock.url : "";
                    this._imageUrlInputElement.value = this.contentBlock.imageUrl != undefined ? this.contentBlock.imageUrl : "";
                    this._heading1InputElement.value = this.contentBlock.heading1 != undefined ? this.contentBlock.heading1 : "";
                    this._heading2InputElement.value = this.contentBlock.heading2 != undefined ? this.contentBlock.heading2 : "";
                    this.htmlContentEditor.setHTML(this.contentBlock.htmlContent != undefined ? this.contentBlock.htmlContent : "");
                    this._titleElement.textContent = this.contentBlockId ? `Edit Content Block: ${this.contentBlock.name}` : "Create Content Block";
                }
                break;
        }           
    }

    public contentBlockId: any;
    
    public contentBlock: ContentBlock;

    public htmlContentEditor: EditorComponent;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".content-block-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".content-block-name") as HTMLInputElement; }

    private get _urlInputElement(): HTMLInputElement { return this.querySelector(".content-block-url") as HTMLInputElement; }

    private get _imageUrlInputElement(): HTMLInputElement { return this.querySelector(".content-block-image-url") as HTMLInputElement; }

    private get _htmlContentInputElement(): HTMLInputElement { return this.querySelector(".content-block-html-content") as HTMLInputElement; }

    private get _heading1InputElement(): HTMLInputElement { return this.querySelector(".content-block-heading-1") as HTMLInputElement; }

    private get _heading2InputElement(): HTMLInputElement { return this.querySelector(".content-block-heading-2") as HTMLInputElement; }
        
}

customElements.define(`ce-content-block-edit-embed`,ContentBlockEditEmbedComponent);
