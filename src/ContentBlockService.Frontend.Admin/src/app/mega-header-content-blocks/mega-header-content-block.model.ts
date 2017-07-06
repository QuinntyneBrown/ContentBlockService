
export class MegaHeaderContentBlock { 

    public id:any;
    
    public name:string;

    public static fromJSON(data: any): MegaHeaderContentBlock {

        let megaHeaderContentBlock = new MegaHeaderContentBlock();

        megaHeaderContentBlock.id = data.id;

        megaHeaderContentBlock.name = data.name;

        return megaHeaderContentBlock;
    }
}
