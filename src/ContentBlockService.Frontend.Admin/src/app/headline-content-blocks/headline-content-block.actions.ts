import { HeadlineContentBlock } from "./headline-content-block.model";

export const headlineContentBlockActions = {
    ADD: "[HeadlineContentBlock] Add",
    EDIT: "[HeadlineContentBlock] Edit",
    DELETE: "[HeadlineContentBlock] Delete",
    HEADLINE_CONTENT_BLOCKS_CHANGED: "[HeadlineContentBlock] HeadlineContentBlocks Changed"
};

export class HeadlineContentBlockEvent extends CustomEvent {
    constructor(eventName:string, headlineContentBlock: HeadlineContentBlock) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { headlineContentBlock }
        });
    }
}

export class HeadlineContentBlockAdd extends HeadlineContentBlockEvent {
    constructor(headlineContentBlock: HeadlineContentBlock) {
        super(headlineContentBlockActions.ADD, headlineContentBlock);        
    }
}

export class HeadlineContentBlockEdit extends HeadlineContentBlockEvent {
    constructor(headlineContentBlock: HeadlineContentBlock) {
        super(headlineContentBlockActions.EDIT, headlineContentBlock);
    }
}

export class HeadlineContentBlockDelete extends HeadlineContentBlockEvent {
    constructor(headlineContentBlock: HeadlineContentBlock) {
        super(headlineContentBlockActions.DELETE, headlineContentBlock);
    }
}

export class HeadlineContentBlocksChanged extends CustomEvent {
    constructor(headlineContentBlocks: Array<HeadlineContentBlock>) {
        super(headlineContentBlockActions.HEADLINE_CONTENT_BLOCKS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { headlineContentBlocks }
        });
    }
}
