import { MegaHeaderContentBlockAdd, MegaHeaderContentBlockDelete, MegaHeaderContentBlockEdit, megaHeaderContentBlockActions } from "./mega-header-content-block.actions";
import { MegaHeaderContentBlock } from "./mega-header-content-block.model";
import { MegaHeaderContentBlockService } from "./mega-header-content-block.service";

const template = require("./mega-header-content-block-master-detail.component.html");
const styles = require("./mega-header-content-block-master-detail.component.css");

export class MegaHeaderContentBlockMasterDetailComponent extends HTMLElement {
    constructor(
        private _megaHeaderContentBlockService: MegaHeaderContentBlockService = MegaHeaderContentBlockService.Instance	
	) {
        super();
        this.onMegaHeaderContentBlockAdd = this.onMegaHeaderContentBlockAdd.bind(this);
        this.onMegaHeaderContentBlockEdit = this.onMegaHeaderContentBlockEdit.bind(this);
        this.onMegaHeaderContentBlockDelete = this.onMegaHeaderContentBlockDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "mega-header-content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.megaHeaderContentBlocks = await this._megaHeaderContentBlockService.get();
        this.megaHeaderContentBlockListElement.setAttribute("mega-header-content-blocks", JSON.stringify(this.megaHeaderContentBlocks));
    }

    private _setEventListeners() {
        this.addEventListener(megaHeaderContentBlockActions.ADD, this.onMegaHeaderContentBlockAdd);
        this.addEventListener(megaHeaderContentBlockActions.EDIT, this.onMegaHeaderContentBlockEdit);
        this.addEventListener(megaHeaderContentBlockActions.DELETE, this.onMegaHeaderContentBlockDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(megaHeaderContentBlockActions.ADD, this.onMegaHeaderContentBlockAdd);
        this.removeEventListener(megaHeaderContentBlockActions.EDIT, this.onMegaHeaderContentBlockEdit);
        this.removeEventListener(megaHeaderContentBlockActions.DELETE, this.onMegaHeaderContentBlockDelete);
    }

    public async onMegaHeaderContentBlockAdd(e) {

        await this._megaHeaderContentBlockService.add(e.detail.megaHeaderContentBlock);
        this.megaHeaderContentBlocks = await this._megaHeaderContentBlockService.get();
        
        this.megaHeaderContentBlockListElement.setAttribute("mega-header-content-blocks", JSON.stringify(this.megaHeaderContentBlocks));
        this.megaHeaderContentBlockEditElement.setAttribute("mega-header-content-block", JSON.stringify(new MegaHeaderContentBlock()));
    }

    public onMegaHeaderContentBlockEdit(e) {
        this.megaHeaderContentBlockEditElement.setAttribute("mega-header-content-block", JSON.stringify(e.detail.megaHeaderContentBlock));
    }

    public async onMegaHeaderContentBlockDelete(e) {

        await this._megaHeaderContentBlockService.remove(e.detail.megaHeaderContentBlock.id);
        this.megaHeaderContentBlocks = await this._megaHeaderContentBlockService.get();
        
        this.megaHeaderContentBlockListElement.setAttribute("mega-header-content-blocks", JSON.stringify(this.megaHeaderContentBlocks));
        this.megaHeaderContentBlockEditElement.setAttribute("mega-header-content-block", JSON.stringify(new MegaHeaderContentBlock()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "mega-header-content-blocks":
                this.megaHeaderContentBlocks = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<MegaHeaderContentBlock> { return this.megaHeaderContentBlocks; }

    private megaHeaderContentBlocks: Array<MegaHeaderContentBlock> = [];
    public megaHeaderContentBlock: MegaHeaderContentBlock = <MegaHeaderContentBlock>{};
    public get megaHeaderContentBlockEditElement(): HTMLElement { return this.querySelector("ce-mega-header-content-block-edit-embed") as HTMLElement; }
    public get megaHeaderContentBlockListElement(): HTMLElement { return this.querySelector("ce-mega-header-content-block-paginated-list-embed") as HTMLElement; }
}

customElements.define(`ce-mega-header-content-block-master-detail`,MegaHeaderContentBlockMasterDetailComponent);
