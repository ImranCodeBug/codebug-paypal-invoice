export default interface IXrmConnection{
    retrieve : (entityLogicalName : string, id : string, columns : string) => Promise<any>
}