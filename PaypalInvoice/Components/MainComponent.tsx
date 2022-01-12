import * as React from 'react'

interface Props {
    context : any;
}

export const MainComponent = (props: Props) => {
    //console.log(props.context);
    return (
        <div>
            <button type="button" className="btn btn-primary">Primary</button>
        </div>
    )
}
