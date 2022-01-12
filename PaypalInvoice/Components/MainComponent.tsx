import * as React from 'react'
import IXrmConnectionFactory from '../Connectors/Interfaces/iXrmConnectionFactory'
import { XrmServices } from '../Services/XrmServices'

interface Props {
    context : any;
    xrmConnectionFactory : IXrmConnectionFactory
}

export const MainComponent = (props: Props) => {    
    const xrmServices =  XrmServices(props.xrmConnectionFactory.xrmConnection);
    const id : string = props.context.mode.contextInfo.entityId!
    const entityLogicalName : string = props.context.mode.contextInfo.entityTypeName!

    React.useMemo(() => {
        const getInvoiceDetails = async() => {
            const response = await xrmServices.getInvoiceDetails(entityLogicalName, id)
        }
        getInvoiceDetails();
    }, [id])

    

    return (
        <div>
            <button type="button" className="btn btn-primary">Primary</button>
        </div>
    )
}
