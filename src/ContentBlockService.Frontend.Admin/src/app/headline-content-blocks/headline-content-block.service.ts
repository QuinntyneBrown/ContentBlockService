import { fetch } from "../utilities";
import { HeadlineContentBlock } from "./headline-content-block.model";
import { environment } from "../environment";

export class HeadlineContentBlockService {
    constructor(private _fetch = fetch) { }

    private static _instance: HeadlineContentBlockService;

    public static get Instance() {
        this._instance = this._instance || new HeadlineContentBlockService();
        return this._instance;
    }

    public get(): Promise<Array<HeadlineContentBlock>> {
        return this._fetch({ url: `${environment.baseUrl}api/headlinecontentblock/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { headlineContentBlocks: Array<HeadlineContentBlock> }).headlineContentBlocks;
        });
    }

    public getById(id): Promise<HeadlineContentBlock> {
        return this._fetch({ url: `${environment.baseUrl}api/headlinecontentblock/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { headlineContentBlock: HeadlineContentBlock }).headlineContentBlock;
        });
    }

    public add(headlineContentBlock) {
        return this._fetch({ url: `${environment.baseUrl}api/headlinecontentblock/add`, method: `POST`, data: { headlineContentBlock }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/headlinecontentblock/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
