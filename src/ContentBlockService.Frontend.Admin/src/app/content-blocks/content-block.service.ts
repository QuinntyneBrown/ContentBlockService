import { fetch } from "../utilities";
import { ContentBlock } from "./content-block.model";

export class ContentBlockService {
    constructor(private _fetch = fetch) { }

    private static _instance: ContentBlockService;

    public static get Instance() {
        this._instance = this._instance || new ContentBlockService();
        return this._instance;
    }

    public get(): Promise<Array<ContentBlock>> {
        return this._fetch({ url: "/api/contentblock/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { contentBlocks: Array<ContentBlock> }).contentBlocks;
        });
    }

    public getById(id): Promise<ContentBlock> {
        return this._fetch({ url: `/api/contentblock/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { contentBlock: ContentBlock }).contentBlock;
        });
    }

    public add(contentBlock) {
        return this._fetch({ url: `/api/contentblock/add`, method: "POST", data: { contentBlock }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/contentblock/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
