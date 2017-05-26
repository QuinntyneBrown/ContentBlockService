import { HeadlineContentBlockAdd, HeadlineContentBlockDelete, HeadlineContentBlockEdit, headlineContentBlockActions } from "./headline-content-block.actions";
import { HeadlineContentBlock } from "./headline-content-block.model";
import { HeadlineContentBlockService } from "./headline-content-block.service";

const template = require("./headline-content-block-master-detail.component.html");
const styles = require("./headline-content-block-master-detail.component.scss");

export class HeadlineContentBlockMasterDetailComponent extends HTMLElement {
    constructor(
        private _headlineContentBlockService: HeadlineContentBlockService = HeadlineContentBlockService.Instance	
	) {
        super();
        this.onHeadlineContentBlockAdd = this.onHeadlineContentBlockAdd.bind(this);
        this.onHeadlineContentBlockEdit = this.onHeadlineContentBlockEdit.bind(this);
        this.onHeadlineContentBlockDelete = this.onHeadlineContentBlockDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "headline-content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.headlineContentBlocks = await this._headlineContentBlockService.get();
        this.headlineContentBlockListElement.setAttribute("headline-content-blocks", JSON.stringify(this.headlineContentBlocks));
    }

    private _setEventListeners() {
        this.addEventListener(headlineContentBlockActions.ADD, this.onHeadlineContentBlockAdd);
        this.addEventListener(headlineContentBlockActions.EDIT, this.onHeadlineContentBlockEdit);
        this.addEventListener(headlineContentBlockActions.DELETE, this.onHeadlineContentBlockDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(headlineContentBlockActions.ADD, this.onHeadlineContentBlockAdd);
        this.removeEventListener(headlineContentBlockActions.EDIT, this.onHeadlineContentBlockEdit);
        this.removeEventListener(headlineContentBlockActions.DELETE, this.onHeadlineContentBlockDelete);
    }

    public async onHeadlineContentBlockAdd(e) {

        await this._headlineContentBlockService.add(e.detail.headlineContentBlock);
        this.headlineContentBlocks = await this._headlineContentBlockService.get();
        
        this.headlineContentBlockListElement.setAttribute("headline-content-blocks", JSON.stringify(this.headlineContentBlocks));
        this.headlineContentBlockEditElement.setAttribute("headline-content-block", JSON.stringify(new HeadlineContentBlock()));
    }

    public onHeadlineContentBlockEdit(e) {
        this.headlineContentBlockEditElement.setAttribute("headline-content-block", JSON.stringify(e.detail.headlineContentBlock));
    }

    public async onHeadlineContentBlockDelete(e) {

        await this._headlineContentBlockService.remove(e.detail.headlineContentBlock.id);
        this.headlineContentBlocks = await this._headlineContentBlockService.get();
        
        this.headlineContentBlockListElement.setAttribute("headline-content-blocks", JSON.stringify(this.headlineContentBlocks));
        this.headlineContentBlockEditElement.setAttribute("headline-content-block", JSON.stringify(new HeadlineContentBlock()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "headline-content-blocks":
                this.headlineContentBlocks = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<HeadlineContentBlock> { return this.headlineContentBlocks; }

    private headlineContentBlocks: Array<HeadlineContentBlock> = [];
    public headlineContentBlock: HeadlineContentBlock = <HeadlineContentBlock>{};
    public get headlineContentBlockEditElement(): HTMLElement { return this.querySelector("ce-headline-content-block-edit-embed") as HTMLElement; }
    public get headlineContentBlockListElement(): HTMLElement { return this.querySelector("ce-headline-content-block-paginated-list-embed") as HTMLElement; }
}

customElements.define(`ce-headline-content-block-master-detail`,HeadlineContentBlockMasterDetailComponent);
