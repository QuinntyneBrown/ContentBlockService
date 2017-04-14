export class RestService { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): RestService {
        let restService = new RestService();
        restService.name = data.name;
        return restService;
    }
}
