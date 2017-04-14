import { ContentBlock } from "./content-block.model";

const template = require("./content-block-list-embed.component.html");
const styles = require("./content-block-list-embed.component.scss");

export class ContentBlockListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.contentBlocks.length; i++) {
            let el = this._document.createElement(`ce-content-block-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.contentBlocks[i]));
            this.appendChild(el);
        }    
    }

    contentBlocks:Array<ContentBlock> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "content-blocks":
                this.contentBlocks = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-content-block-list-embed", ContentBlockListEmbedComponent);
