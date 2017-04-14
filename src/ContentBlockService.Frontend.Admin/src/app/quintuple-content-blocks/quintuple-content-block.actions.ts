import { QuintupleContentBlock } from "./quintuple-content-block.model";

export const quintupleContentBlockActions = {
    ADD: "[QuintupleContentBlock] Add",
    EDIT: "[QuintupleContentBlock] Edit",
    DELETE: "[QuintupleContentBlock] Delete",
    QUINTUPLE_CONTENT_BLOCKS_CHANGED: "[QuintupleContentBlock] QuintupleContentBlocks Changed"
};

export class QuintupleContentBlockEvent extends CustomEvent {
    constructor(eventName:string, quintupleContentBlock: QuintupleContentBlock) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { quintupleContentBlock }
        });
    }
}

export class QuintupleContentBlockAdd extends QuintupleContentBlockEvent {
    constructor(quintupleContentBlock: QuintupleContentBlock) {
        super(quintupleContentBlockActions.ADD, quintupleContentBlock);        
    }
}

export class QuintupleContentBlockEdit extends QuintupleContentBlockEvent {
    constructor(quintupleContentBlock: QuintupleContentBlock) {
        super(quintupleContentBlockActions.EDIT, quintupleContentBlock);
    }
}

export class QuintupleContentBlockDelete extends QuintupleContentBlockEvent {
    constructor(quintupleContentBlock: QuintupleContentBlock) {
        super(quintupleContentBlockActions.DELETE, quintupleContentBlock);
    }
}

export class QuintupleContentBlocksChanged extends CustomEvent {
    constructor(quintupleContentBlocks: Array<QuintupleContentBlock>) {
        super(quintupleContentBlockActions.QUINTUPLE_CONTENT_BLOCKS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { quintupleContentBlocks }
        });
    }
}
