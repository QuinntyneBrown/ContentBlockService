import { QuintupleContentBlockAdd, QuintupleContentBlockDelete, QuintupleContentBlockEdit, quintupleContentBlockActions } from "./quintuple-content-block.actions";
import { QuintupleContentBlock } from "./quintuple-content-block.model";
import { QuintupleContentBlockService } from "./quintuple-content-block.service";

const template = require("./quintuple-content-block-master-detail.component.html");
const styles = require("./quintuple-content-block-master-detail.component.scss");

export class QuintupleContentBlockMasterDetailComponent extends HTMLElement {
    constructor(
        private _quintupleContentBlockService: QuintupleContentBlockService = QuintupleContentBlockService.Instance	
	) {
        super();
        this.onQuintupleContentBlockAdd = this.onQuintupleContentBlockAdd.bind(this);
        this.onQuintupleContentBlockEdit = this.onQuintupleContentBlockEdit.bind(this);
        this.onQuintupleContentBlockDelete = this.onQuintupleContentBlockDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "quintuple-content-blocks"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.quintupleContentBlocks = await this._quintupleContentBlockService.get();
        this.quintupleContentBlockListElement.setAttribute("quintuple-content-blocks", JSON.stringify(this.quintupleContentBlocks));
    }

    private _setEventListeners() {
        this.addEventListener(quintupleContentBlockActions.ADD, this.onQuintupleContentBlockAdd);
        this.addEventListener(quintupleContentBlockActions.EDIT, this.onQuintupleContentBlockEdit);
        this.addEventListener(quintupleContentBlockActions.DELETE, this.onQuintupleContentBlockDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(quintupleContentBlockActions.ADD, this.onQuintupleContentBlockAdd);
        this.removeEventListener(quintupleContentBlockActions.EDIT, this.onQuintupleContentBlockEdit);
        this.removeEventListener(quintupleContentBlockActions.DELETE, this.onQuintupleContentBlockDelete);
    }

    public async onQuintupleContentBlockAdd(e) {

        await this._quintupleContentBlockService.add(e.detail.quintupleContentBlock);
        this.quintupleContentBlocks = await this._quintupleContentBlockService.get();
        
        this.quintupleContentBlockListElement.setAttribute("quintuple-content-blocks", JSON.stringify(this.quintupleContentBlocks));
        this.quintupleContentBlockEditElement.setAttribute("quintuple-content-block", JSON.stringify(new QuintupleContentBlock()));
    }

    public onQuintupleContentBlockEdit(e) {
        this.quintupleContentBlockEditElement.setAttribute("quintuple-content-block", JSON.stringify(e.detail.quintupleContentBlock));
    }

    public async onQuintupleContentBlockDelete(e) {

        await this._quintupleContentBlockService.remove(e.detail.quintupleContentBlock.id);
        this.quintupleContentBlocks = await this._quintupleContentBlockService.get();
        
        this.quintupleContentBlockListElement.setAttribute("quintuple-content-blocks", JSON.stringify(this.quintupleContentBlocks));
        this.quintupleContentBlockEditElement.setAttribute("quintuple-content-block", JSON.stringify(new QuintupleContentBlock()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "quintuple-content-blocks":
                this.quintupleContentBlocks = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<QuintupleContentBlock> { return this.quintupleContentBlocks; }

    private quintupleContentBlocks: Array<QuintupleContentBlock> = [];
    public quintupleContentBlock: QuintupleContentBlock = <QuintupleContentBlock>{};
    public get quintupleContentBlockEditElement(): HTMLElement { return this.querySelector("ce-quintuple-content-block-edit-embed") as HTMLElement; }
    public get quintupleContentBlockListElement(): HTMLElement { return this.querySelector("ce-quintuple-content-block-list-embed") as HTMLElement; }
}

customElements.define(`ce-quintuple-content-block-master-detail`,QuintupleContentBlockMasterDetailComponent);
