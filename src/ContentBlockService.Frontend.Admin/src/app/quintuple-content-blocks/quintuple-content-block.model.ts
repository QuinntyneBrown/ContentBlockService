export class QuintupleContentBlock { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): QuintupleContentBlock {
        let quintupleContentBlock = new QuintupleContentBlock();
        quintupleContentBlock.name = data.name;
        return quintupleContentBlock;
    }
}
