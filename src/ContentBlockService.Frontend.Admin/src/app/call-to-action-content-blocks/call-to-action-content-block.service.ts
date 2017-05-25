import { fetch } from "../utilities";
import { CallToActionContentBlock } from "./call-to-action-content-block.model";
import { environment } from "../environment";

export class CallToActionContentBlockService {
    constructor(private _fetch = fetch) { }

    private static _instance: CallToActionContentBlockService;

    public static get Instance() {
        this._instance = this._instance || new CallToActionContentBlockService();
        return this._instance;
    }

    public get(): Promise<Array<CallToActionContentBlock>> {
        return this._fetch({ url: `${environment.baseUrl}api/calltoactioncontentblock/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { callToActionContentBlocks: Array<CallToActionContentBlock> }).callToActionContentBlocks;
        });
    }

    public getById(id): Promise<CallToActionContentBlock> {
        return this._fetch({ url: `${environment.baseUrl}api/calltoactioncontentblock/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { callToActionContentBlock: CallToActionContentBlock }).callToActionContentBlock;
        });
    }

    public add(callToActionContentBlock) {
        return this._fetch({ url: `${environment.baseUrl}api/calltoactioncontentblock/add`, method: `POST`, data: { callToActionContentBlock }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/calltoactioncontentblock/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
