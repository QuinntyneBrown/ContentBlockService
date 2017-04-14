import { RestService } from "./rest-service.model";

export const restServiceActions = {
    ADD: "[RestService] Add",
    EDIT: "[RestService] Edit",
    DELETE: "[RestService] Delete",
    REST_SERVICES_CHANGED: "[RestService] RestServices Changed"
};

export class RestServiceEvent extends CustomEvent {
    constructor(eventName:string, restService: RestService) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { restService }
        });
    }
}

export class RestServiceAdd extends RestServiceEvent {
    constructor(restService: RestService) {
        super(restServiceActions.ADD, restService);        
    }
}

export class RestServiceEdit extends RestServiceEvent {
    constructor(restService: RestService) {
        super(restServiceActions.EDIT, restService);
    }
}

export class RestServiceDelete extends RestServiceEvent {
    constructor(restService: RestService) {
        super(restServiceActions.DELETE, restService);
    }
}

export class RestServicesChanged extends CustomEvent {
    constructor(restServices: Array<RestService>) {
        super(restServiceActions.REST_SERVICES_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { restServices }
        });
    }
}
