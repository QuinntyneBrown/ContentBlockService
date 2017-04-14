export class ContentBlock { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): ContentBlock {
        let contentBlock = new ContentBlock();
        contentBlock.name = data.name;
        return contentBlock;
    }
}
