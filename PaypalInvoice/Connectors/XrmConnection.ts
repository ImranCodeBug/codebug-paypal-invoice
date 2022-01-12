import IXrmConnection from "./Interfaces/IXrmConnection";

export default class XrmConnection implements IXrmConnection{
    private readonly _webApi : ComponentFramework.WebApi;

    constructor(webApi : ComponentFramework.WebApi){
        this._webApi = webApi;
    }

    retrieve = async(entityLogicalName : string, id : string, columns : string) => {
        const result = await this._webApi.retrieveRecord(entityLogicalName, id, columns)
                        .then(response => {
                            console.log(response);
                            return 6;
                        })
        return result;
    }
}
