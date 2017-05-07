export class ContentBlock {

    public id: any;

    public name: string;

    public imageUrl: string;

    public url: string;

    public htmlContent: string;

    public heading1: any;

    public heading2: any;

    public fromJSON(data: any): ContentBlock {

        let contentBlock = new ContentBlock();

        contentBlock.name = data.name;

        contentBlock.imageUrl = data.imageUrl;

        contentBlock.url = data.url;

        contentBlock.htmlContent = data.htmlContent;

        contentBlock.heading1 = data.heading1;

        contentBlock.heading2 = data.heading2;

        return contentBlock;
    }
}
