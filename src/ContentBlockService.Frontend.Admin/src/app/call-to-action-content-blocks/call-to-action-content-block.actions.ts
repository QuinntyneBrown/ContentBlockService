import { CallToActionContentBlock } from "./call-to-action-content-block.model";

export const callToActionContentBlockActions = {
    ADD: "[CallToActionContentBlock] Add",
    EDIT: "[CallToActionContentBlock] Edit",
    DELETE: "[CallToActionContentBlock] Delete",
    CALL_TO_ACTION_CONTENT_BLOCKS_CHANGED: "[CallToActionContentBlock] CallToActionContentBlocks Changed"
};

export class CallToActionContentBlockEvent extends CustomEvent {
    constructor(eventName:string, callToActionContentBlock: CallToActionContentBlock) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { callToActionContentBlock }
        });
    }
}

export class CallToActionContentBlockAdd extends CallToActionContentBlockEvent {
    constructor(callToActionContentBlock: CallToActionContentBlock) {
        super(callToActionContentBlockActions.ADD, callToActionContentBlock);        
    }
}

export class CallToActionContentBlockEdit extends CallToActionContentBlockEvent {
    constructor(callToActionContentBlock: CallToActionContentBlock) {
        super(callToActionContentBlockActions.EDIT, callToActionContentBlock);
    }
}

export class CallToActionContentBlockDelete extends CallToActionContentBlockEvent {
    constructor(callToActionContentBlock: CallToActionContentBlock) {
        super(callToActionContentBlockActions.DELETE, callToActionContentBlock);
    }
}

export class CallToActionContentBlocksChanged extends CustomEvent {
    constructor(callToActionContentBlocks: Array<CallToActionContentBlock>) {
        super(callToActionContentBlockActions.CALL_TO_ACTION_CONTENT_BLOCKS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { callToActionContentBlocks }
        });
    }
}
