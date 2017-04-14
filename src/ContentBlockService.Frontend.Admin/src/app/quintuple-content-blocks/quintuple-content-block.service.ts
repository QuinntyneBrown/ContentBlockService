import { fetch } from "../utilities";
import { QuintupleContentBlock } from "./quintuple-content-block.model";

export class QuintupleContentBlockService {
    constructor(private _fetch = fetch) { }

    private static _instance: QuintupleContentBlockService;

    public static get Instance() {
        this._instance = this._instance || new QuintupleContentBlockService();
        return this._instance;
    }

    public get(): Promise<Array<QuintupleContentBlock>> {
        return this._fetch({ url: "/api/quintuplecontentblock/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { quintupleContentBlocks: Array<QuintupleContentBlock> }).quintupleContentBlocks;
        });
    }

    public getById(id): Promise<QuintupleContentBlock> {
        return this._fetch({ url: `/api/quintuplecontentblock/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { quintupleContentBlock: QuintupleContentBlock }).quintupleContentBlock;
        });
    }

    public add(quintupleContentBlock) {
        return this._fetch({ url: `/api/quintuplecontentblock/add`, method: "POST", data: { quintupleContentBlock }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/quintuplecontentblock/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
