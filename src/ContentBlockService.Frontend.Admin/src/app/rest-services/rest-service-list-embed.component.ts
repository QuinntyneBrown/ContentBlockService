import { RestService } from "./rest-service.model";

const template = require("./rest-service-list-embed.component.html");
const styles = require("./rest-service-list-embed.component.scss");

export class RestServiceListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "rest-services"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.restServices.length; i++) {
            let el = this._document.createElement(`ce-rest-service-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.restServices[i]));
            this.appendChild(el);
        }    
    }

    restServices:Array<RestService> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "rest-services":
                this.restServices = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-rest-service-list-embed", RestServiceListEmbedComponent);
