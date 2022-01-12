import IXrmConnection from "../Connectors/Interfaces/IXrmConnection"

export const XrmServices = (xrmConnection : IXrmConnection) => {
    const invoiceColumns = "?$select=billto_composite,billto_city,billto_country,billto_fax,billto_name,billto_telephone,billto_stateorprovince,billto_line1,billto_line2,billto_line3,billto_postalcode,_transactioncurrencyid_value,_customerid_value,datedelivered,description,duedate,emailaddress,exchangerate,freightamount,freightamount_base,discountpercentage,discountamount,discountamount_base,invoicenumber,willcall,shipto_composite,shipto_city,shipto_country,shipto_fax,shipto_freighttermscode,shipto_name,shipto_telephone,shipto_stateorprovince,shipto_line1,shipto_line2,shipto_line3,shipto_postalcode,statecode,statuscode,totalamount,totalamount_base,totallineitemamount,totallineitemamount_base,totaldiscountamount,totaldiscountamount_base,totallineitemdiscountamount,totallineitemdiscountamount_base,totalamountlessfreight,totalamountlessfreight_base,totaltax,totaltax_base";
    
    
    const getInvoiceDetails = async(entityLogicalName : string, id : string) => {
            return await xrmConnection.retrieve(entityLogicalName, id, invoiceColumns)        
    }
    
    return {
        getInvoiceDetails : getInvoiceDetails
    }
}