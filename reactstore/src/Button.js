import style from 'styled-components'
export const ButtonContainer = style.button`
text-transform:capitalize;
font-size:1.4rem;
background:transparent:
border:.05rem solid var(--lightBlue);

border-radius:.5rem;
padding:.2rem .5rem .2rem 0;
transitional:all .5s ease-in-out;
margin: .2rem .5rem .2rem 0;

border-color:${props => (props.cart ? 'var(--mainYellow)' : 'var(--lightBlue)')}

color: ${(prop) => {
 return( (prop.cart ? 'var(--mainYellow)' : 'var(--lightBlue)'))
}};

&:hover{
    background:${(prop) => {
      return (prop.cart ? 'var(--mainYellow)' : 'var(--lightBlue)')
    }}; 
    color: var(--mainBlue);
}
&:focus{
    outline:none; 
}
`;
