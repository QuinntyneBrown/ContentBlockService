import { MegaHeaderContentBlock } from "./mega-header-content-block.model";
import {  MegaHeaderContentBlockDelete, MegaHeaderContentBlockEdit, MegaHeaderContentBlockAdd } from "./mega-header-content-block.actions";
	
const template = require("./mega-header-content-block-item-embed.component.html");
const styles = require("./mega-header-content-block-item-embed.component.css");

export class MegaHeaderContentBlockItemEmbedComponent extends HTMLElement {
    constructor() {
        super();

        this._onDeleteClick = this._onDeleteClick.bind(this);
        this._onEditClick = this._onEditClick.bind(this);
        this._onViewClick = this._onViewClick.bind(this);
    }

    static get observedAttributes() {
        return ["entity"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    disconnectedCallback() {
        this._deleteLinkElement.removeEventListener("click", this._onDeleteClick);
        this._editLinkElement.removeEventListener("click", this._onEditClick);
        this._viewLinkElement.removeEventListener("click", this._onViewClick);
    }

    private _bind() {
        this._nameElement.textContent = this.entity.name;
    }

    private _setEventListeners() {
        this._deleteLinkElement.addEventListener("click", this._onDeleteClick);
        this._editLinkElement.addEventListener("click", this._onEditClick);
        this._viewLinkElement.addEventListener("click", this._onViewClick);
    }

    private async _onDeleteClick(e:Event) {
        this.dispatchEvent(new MegaHeaderContentBlockDelete(this.entity)); 
    }

    private _onEditClick() {
        this.dispatchEvent(new MegaHeaderContentBlockEdit(this.entity));
    }

    private _onViewClick() {

    }
    
    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "entity":
                this.entity = JSON.parse(newValue);
                break;
        }        
    }

    private get _nameElement() { return this.querySelector("p") as HTMLElement; }
    private get _deleteLinkElement() { return this.querySelector(".entity-item-delete") as HTMLElement; }
    private get _editLinkElement() { return this.querySelector(".entity-item-edit") as HTMLElement; }
    private get _viewLinkElement() { return this.querySelector(".entity-item-view") as HTMLElement; }
    public entity: MegaHeaderContentBlock;
}

customElements.define(`ce-mega-header-content-block-item-embed`,MegaHeaderContentBlockItemEmbedComponent);
