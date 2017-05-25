import { CallToActionContentBlockAdd, CallToActionContentBlockDelete, CallToActionContentBlockEdit, callToActionContentBlockActions } from "./call-to-action-content-block.actions";
import { CallToActionContentBlock } from "./call-to-action-content-block.model";
import { CallToActionContentBlockService } from "./call-to-action-content-block.service";

const template = require("./call-to-action-content-block-master-detail.component.html");
const styles = require("./call-to-action-content-block-master-detail.component.scss");

export class CallToActionContentBlockMasterDetailComponent extends HTMLElement {
    constructor(
        private _callToActionContentBlockService: CallToActionContentBlockService = CallToActionContentBlockService.Instance	
	) {
        super();
        this.onCallToActionContentBlockAdd = this.onCallToActionContentBlockAdd.bind(this);
        this.onCallToActionContentBlockEdit = this.onCallToActionContentBlockEdit.bind(this);
        this.onCallToActionContentBlockDelete = this.onCallToActionContentBlockDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "call-to-action-content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.callToActionContentBlocks = await this._callToActionContentBlockService.get();
        this.callToActionContentBlockListElement.setAttribute("call-to-action-content-blocks", JSON.stringify(this.callToActionContentBlocks));
    }

    private _setEventListeners() {
        this.addEventListener(callToActionContentBlockActions.ADD, this.onCallToActionContentBlockAdd);
        this.addEventListener(callToActionContentBlockActions.EDIT, this.onCallToActionContentBlockEdit);
        this.addEventListener(callToActionContentBlockActions.DELETE, this.onCallToActionContentBlockDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(callToActionContentBlockActions.ADD, this.onCallToActionContentBlockAdd);
        this.removeEventListener(callToActionContentBlockActions.EDIT, this.onCallToActionContentBlockEdit);
        this.removeEventListener(callToActionContentBlockActions.DELETE, this.onCallToActionContentBlockDelete);
    }

    public async onCallToActionContentBlockAdd(e) {

        await this._callToActionContentBlockService.add(e.detail.callToActionContentBlock);
        this.callToActionContentBlocks = await this._callToActionContentBlockService.get();
        
        this.callToActionContentBlockListElement.setAttribute("call-to-action-content-blocks", JSON.stringify(this.callToActionContentBlocks));
        this.callToActionContentBlockEditElement.setAttribute("call-to-action-content-block", JSON.stringify(new CallToActionContentBlock()));
    }

    public onCallToActionContentBlockEdit(e) {
        this.callToActionContentBlockEditElement.setAttribute("call-to-action-content-block", JSON.stringify(e.detail.callToActionContentBlock));
    }

    public async onCallToActionContentBlockDelete(e) {

        await this._callToActionContentBlockService.remove(e.detail.callToActionContentBlock.id);
        this.callToActionContentBlocks = await this._callToActionContentBlockService.get();
        
        this.callToActionContentBlockListElement.setAttribute("call-to-action-content-blocks", JSON.stringify(this.callToActionContentBlocks));
        this.callToActionContentBlockEditElement.setAttribute("call-to-action-content-block", JSON.stringify(new CallToActionContentBlock()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "call-to-action-content-blocks":
                this.callToActionContentBlocks = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<CallToActionContentBlock> { return this.callToActionContentBlocks; }

    private callToActionContentBlocks: Array<CallToActionContentBlock> = [];
    public callToActionContentBlock: CallToActionContentBlock = <CallToActionContentBlock>{};
    public get callToActionContentBlockEditElement(): HTMLElement { return this.querySelector("ce-call-to-action-content-block-edit-embed") as HTMLElement; }
    public get callToActionContentBlockListElement(): HTMLElement { return this.querySelector("ce-call-to-action-content-block-paginated-list-embed") as HTMLElement; }
}

customElements.define(`ce-call-to-action-content-block-master-detail`,CallToActionContentBlockMasterDetailComponent);
