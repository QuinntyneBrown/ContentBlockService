export class HeadlineContentBlock { 

    public id:any;
    
    public name:string;

    public headline1: string;

    public headline2: string;

    public slug: string;

    public static fromJSON(data: any): HeadlineContentBlock {

        let headlineContentBlock = new HeadlineContentBlock();

        headlineContentBlock.name = data.name;

        return headlineContentBlock;
    }
}
