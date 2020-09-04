import React from 'react'
import PaypalExpressBtn from 'react-paypal-express-checkout';


export default function PayPalButton()
{
    const onSuccess=(payment)=>
    {
        console.log("this works" ,payment)

    }
    const onCancel=(data)=>
    {
        console.log("this works" ,data)

    }
const onError=(err)=>
{

    console.log("doesn't",err);

}

let env="sandbox";
let currency="USD";
let total=1;

const client={
    sandbox:"Abb_qtm_ocwJSD49XmxrAprX4bWyZXPBnQYe__bE8EZjHt1wj3IpD8rXuVRY2T8SCKn1BXNsfmhB2Iq8",
    production:"test"
}
    return(

        <>
        <PaypalExpressBtn
        env={env}
        client={client}
        currency={currency}
        total={total}
        onCancel={onCancel}
        onError={onError}
        onSuccess={onSuccess}
        />
        </>

    );
}