import { ContentBlockAdd, ContentBlockDelete, ContentBlockEdit, contentBlockActions } from "./content-block.actions";
import { ContentBlock } from "./content-block.model";
import { ContentBlockService } from "./content-block.service";

const template = require("./content-block-master-detail.component.html");
const styles = require("./content-block-master-detail.component.scss");

export class ContentBlockMasterDetailComponent extends HTMLElement {
    constructor(
        private _contentBlockService: ContentBlockService = ContentBlockService.Instance	
	) {
        super();
        this.onContentBlockAdd = this.onContentBlockAdd.bind(this);
        this.onContentBlockEdit = this.onContentBlockEdit.bind(this);
        this.onContentBlockDelete = this.onContentBlockDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.contentBlocks = await this._contentBlockService.get();
        this.contentBlockListElement.setAttribute("content-blocks", JSON.stringify(this.contentBlocks));
    }

    private _setEventListeners() {
        this.addEventListener(contentBlockActions.ADD, this.onContentBlockAdd);
        this.addEventListener(contentBlockActions.EDIT, this.onContentBlockEdit);
        this.addEventListener(contentBlockActions.DELETE, this.onContentBlockDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(contentBlockActions.ADD, this.onContentBlockAdd);
        this.removeEventListener(contentBlockActions.EDIT, this.onContentBlockEdit);
        this.removeEventListener(contentBlockActions.DELETE, this.onContentBlockDelete);
    }

    public async onContentBlockAdd(e) {

        await this._contentBlockService.add(e.detail.contentBlock);
        this.contentBlocks = await this._contentBlockService.get();
        
        this.contentBlockListElement.setAttribute("content-blocks", JSON.stringify(this.contentBlocks));
        this.contentBlockEditElement.setAttribute("content-block", JSON.stringify(new ContentBlock()));
    }

    public onContentBlockEdit(e) {
        this.contentBlockEditElement.setAttribute("content-block", JSON.stringify(e.detail.contentBlock));
    }

    public async onContentBlockDelete(e) {

        await this._contentBlockService.remove(e.detail.contentBlock.id);
        this.contentBlocks = await this._contentBlockService.get();
        
        this.contentBlockListElement.setAttribute("content-blocks", JSON.stringify(this.contentBlocks));
        this.contentBlockEditElement.setAttribute("content-block", JSON.stringify(new ContentBlock()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "content-blocks":
                this.contentBlocks = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<ContentBlock> { return this.contentBlocks; }

    private contentBlocks: Array<ContentBlock> = [];
    public contentBlock: ContentBlock = <ContentBlock>{};
    public get contentBlockEditElement(): HTMLElement { return this.querySelector("ce-content-block-edit-embed") as HTMLElement; }
    public get contentBlockListElement(): HTMLElement { return this.querySelector("ce-content-block-list-embed") as HTMLElement; }
}

customElements.define(`ce-content-block-master-detail`,ContentBlockMasterDetailComponent);
