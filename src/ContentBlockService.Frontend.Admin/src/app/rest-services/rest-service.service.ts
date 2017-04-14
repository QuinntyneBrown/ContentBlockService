import { fetch } from "../utilities";
import { RestService } from "./rest-service.model";

export class RestServiceService {
    constructor(private _fetch = fetch) { }

    private static _instance: RestServiceService;

    public static get Instance() {
        this._instance = this._instance || new RestServiceService();
        return this._instance;
    }

    public get(): Promise<Array<RestService>> {
        return this._fetch({ url: "/api/restservice/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { restServices: Array<RestService> }).restServices;
        });
    }

    public getById(id): Promise<RestService> {
        return this._fetch({ url: `/api/restservice/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { restService: RestService }).restService;
        });
    }

    public add(restService) {
        return this._fetch({ url: `/api/restservice/add`, method: "POST", data: { restService }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/restservice/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
