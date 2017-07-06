import { MegaHeaderContentBlock } from "./mega-header-content-block.model";

export const megaHeaderContentBlockActions = {
    ADD: "[MegaHeaderContentBlock] Add",
    EDIT: "[MegaHeaderContentBlock] Edit",
    DELETE: "[MegaHeaderContentBlock] Delete",
    MEGA_HEADER_CONTENT_BLOCKS_CHANGED: "[MegaHeaderContentBlock] MegaHeaderContentBlocks Changed"
};

export class MegaHeaderContentBlockEvent extends CustomEvent {
    constructor(eventName:string, megaHeaderContentBlock: MegaHeaderContentBlock) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            composed: true,
            detail: { megaHeaderContentBlock }
        } as CustomEventInit);
    }
}

export class MegaHeaderContentBlockAdd extends MegaHeaderContentBlockEvent {
    constructor(megaHeaderContentBlock: MegaHeaderContentBlock) {
        super(megaHeaderContentBlockActions.ADD, megaHeaderContentBlock);        
    }
}

export class MegaHeaderContentBlockEdit extends MegaHeaderContentBlockEvent {
    constructor(megaHeaderContentBlock: MegaHeaderContentBlock) {
        super(megaHeaderContentBlockActions.EDIT, megaHeaderContentBlock);
    }
}

export class MegaHeaderContentBlockDelete extends MegaHeaderContentBlockEvent {
    constructor(megaHeaderContentBlock: MegaHeaderContentBlock) {
        super(megaHeaderContentBlockActions.DELETE, megaHeaderContentBlock);
    }
}

export class MegaHeaderContentBlocksChanged extends CustomEvent {
    constructor(megaHeaderContentBlocks: Array<MegaHeaderContentBlock>) {
        super(megaHeaderContentBlockActions.MEGA_HEADER_CONTENT_BLOCKS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { megaHeaderContentBlocks }
        });
    }
}
