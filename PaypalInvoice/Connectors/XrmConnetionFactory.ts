import IXrmConnection from "./Interfaces/IXrmConnection";
import IXrmConnectionFactory from "./Interfaces/iXrmConnectionFactory";
import XrmConnection from "./XrmConnection";

export default class XrmConnectionFactory implements IXrmConnectionFactory{
    readonly xrmConnection: IXrmConnection;    
    constructor(webApi : ComponentFramework.WebApi){
        this.xrmConnection = new XrmConnection(webApi);
    }

}