import { fetch } from "../utilities";
import { MegaHeaderContentBlock } from "./mega-header-content-block.model";
import { environment } from "../environment";

export class MegaHeaderContentBlockService {
    constructor(private _fetch = fetch) { }

    private static _instance: MegaHeaderContentBlockService;

    public static get Instance() {
        this._instance = this._instance || new MegaHeaderContentBlockService();
        return this._instance;
    }

    public get(): Promise<Array<MegaHeaderContentBlock>> {
        return this._fetch({ url: `${environment.baseUrl}api/megaheadercontentblock/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { megaHeaderContentBlocks: Array<MegaHeaderContentBlock> }).megaHeaderContentBlocks;
        });
    }

    public getById(id): Promise<MegaHeaderContentBlock> {
        return this._fetch({ url: `${environment.baseUrl}api/megaheadercontentblock/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { megaHeaderContentBlock: MegaHeaderContentBlock }).megaHeaderContentBlock;
        });
    }

    public add(megaHeaderContentBlock) {
        return this._fetch({ url: `${environment.baseUrl}api/megaheadercontentblock/add`, method: `POST`, data: { megaHeaderContentBlock }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/megaheadercontentblock/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
