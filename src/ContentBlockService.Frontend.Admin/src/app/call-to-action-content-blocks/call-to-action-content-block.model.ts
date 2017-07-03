export class CallToActionContentBlock { 

    public id:any;
    
    public name: string;

    public headline: string;

    public body: string;

    public buttonCaption: string;

    public finalNote: string;

    public callToAction: string;

    public static fromJSON(data: any): CallToActionContentBlock {

        let callToActionContentBlock = new CallToActionContentBlock();

        callToActionContentBlock.name = data.name;

        callToActionContentBlock.headline = data.headline;

        callToActionContentBlock.body = data.body;

        callToActionContentBlock.buttonCaption = data.buttonCaption;

        callToActionContentBlock.finalNote = data.finalNote;

        callToActionContentBlock.callToAction = data.callToAction;

        return callToActionContentBlock;
    }
}
