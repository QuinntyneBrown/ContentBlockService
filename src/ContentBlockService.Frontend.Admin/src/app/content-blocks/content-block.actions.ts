import { ContentBlock } from "./content-block.model";

export const contentBlockActions = {
    ADD: "[ContentBlock] Add",
    EDIT: "[ContentBlock] Edit",
    DELETE: "[ContentBlock] Delete",
    CONTENT_BLOCKS_CHANGED: "[ContentBlock] ContentBlocks Changed"
};

export class ContentBlockEvent extends CustomEvent {
    constructor(eventName:string, contentBlock: ContentBlock) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { contentBlock }
        });
    }
}

export class ContentBlockAdd extends ContentBlockEvent {
    constructor(contentBlock: ContentBlock) {
        super(contentBlockActions.ADD, contentBlock);        
    }
}

export class ContentBlockEdit extends ContentBlockEvent {
    constructor(contentBlock: ContentBlock) {
        super(contentBlockActions.EDIT, contentBlock);
    }
}

export class ContentBlockDelete extends ContentBlockEvent {
    constructor(contentBlock: ContentBlock) {
        super(contentBlockActions.DELETE, contentBlock);
    }
}

export class ContentBlocksChanged extends CustomEvent {
    constructor(contentBlocks: Array<ContentBlock>) {
        super(contentBlockActions.CONTENT_BLOCKS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { contentBlocks }
        });
    }
}
