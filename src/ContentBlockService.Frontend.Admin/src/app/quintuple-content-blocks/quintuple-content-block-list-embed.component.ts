import { QuintupleContentBlock } from "./quintuple-content-block.model";

const template = require("./quintuple-content-block-list-embed.component.html");
const styles = require("./quintuple-content-block-list-embed.component.scss");

export class QuintupleContentBlockListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "quintuple-content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.quintupleContentBlocks.length; i++) {
            let el = this._document.createElement(`ce-quintuple-content-block-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.quintupleContentBlocks[i]));
            this.appendChild(el);
        }    
    }

    quintupleContentBlocks:Array<QuintupleContentBlock> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "quintuple-content-blocks":
                this.quintupleContentBlocks = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-quintuple-content-block-list-embed", QuintupleContentBlockListEmbedComponent);
