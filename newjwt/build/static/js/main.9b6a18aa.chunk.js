(this.webpackJsonpnewjwt=this.webpackJsonpnewjwt||[]).push([[0],{32:function(e,t,a){},33:function(e,t,a){},55:function(e,t,a){"use strict";a.r(t);var n=a(0),s=a(27),c=a.n(s),r=(a(32),a(5)),l=(a.p,a(33),a(3)),i=a(17),o=a(8),j=a.n(o),b=a(13),m=a(2),d=a(1);j.a.defaults.withCredentials=!0;var u=function(e){var t=Object(m.f)(),a=Object(n.useState)({email:"",password:""}),s=Object(r.a)(a,2),c=s[0],o=s[1],b=Object(n.useState)(!1),u=Object(r.a)(b,2),h=u[0],O=u[1],f=Object(i.a)(),g=f.register,p=(f.handleSubmit,f.watch,f.formState.errors,function(t){t.preventDefault(),function(t,a){try{j.a.post(t,a,{withCredentials:!0,headers:{"Access-Control-Allow-Origin":"https://jwtauthfrontend.azurewebsites.net","Access-Control-Allow-Credentials":!0}}).then((function(t){e.setName(t.data.name),O(!0)})).catch((function(e){}))}catch(n){}}("https://jwtauthfrontend.azurewebsites.net/api/auth/login",c)});return h&&t("/"),Object(d.jsx)(d.Fragment,{children:Object(d.jsx)("main",{className:"form-signin",children:Object(d.jsxs)("form",{onSubmit:p,children:[Object(d.jsx)("h1",{className:"h3 mb-3 fw-normal",children:"Please sign in"}),Object(d.jsxs)("div",{className:"form-floating",children:[Object(d.jsx)("input",Object(l.a)(Object(l.a)({type:"email"},g("email",{pattern:{value:"^([a-z]){1,}@([a-z]){3,8}(.)([a-z]){3,5}$",message:"email"}})),{},{className:"form-control",id:"floatingInput",placeholder:"name@example.com",onChange:function(e){o(Object(l.a)(Object(l.a)({},c),{},{email:e.target.value})),console.log(c)}})),Object(d.jsx)("label",{for:"floatingInput",children:"Email address"})]}),Object(d.jsxs)("div",{className:"form-floating",children:[Object(d.jsx)("input",Object(l.a)(Object(l.a)({type:"password"},g("password",{required:!0,pattern:"^[a-zA-z]+$/i"})),{},{className:"form-control",id:"floatingPassword",placeholder:"Password",onChange:function(e){o(Object(l.a)(Object(l.a)({},c),{},{password:e.target.value})),console.log(c)}})),Object(d.jsx)("label",{for:"floatingPassword",children:"Password"})]}),Object(d.jsx)("div",{className:"checkbox mb-3",children:Object(d.jsxs)("label",{children:[Object(d.jsx)("input",{type:"checkbox",value:"remember-me"})," Remember me please"]})}),Object(d.jsx)("button",{className:"w-100 btn btn-lg btn-primary",type:"submit",onClick:p,children:"Sign in"}),Object(d.jsx)("p",{className:"mt-5 mb-3 text-muted",children:"\xa9 2017\u20132021"})]})})})},h={"Content-Type":"application/json","Access-Control-Allow-Origin":"*"};var O=function(){var e=Object(n.useState)({name:"",email:"bob@gmail.com",password:""}),t=Object(r.a)(e,2),a=t[0],s=t[1],c=Object(n.useState)(!1),o=Object(r.a)(c,2),b=o[0],u=o[1],O=Object(n.createRef)(null),f=Object(i.a)(),g=f.register,p=(f.handleSubmit,f.watch,f.formState.errors,function(e){var t,n;e.preventDefault(),t="https://jwtauthfrontend.azurewebsites.net/api/auth/register",n=a,j.a.post(t,n,h).then((function(e){console.log(e.data),u(!0)})).catch((function(e){console.log(e)}))}),x=Object(m.f)();return 1==b&&x("/login"),Object(d.jsx)(d.Fragment,{children:Object(d.jsx)("main",{className:"form-signin",children:Object(d.jsxs)("form",{onSubmit:p,children:[Object(d.jsx)("h1",{className:"h3 mb-3 fw-normal",children:"Sign Up This Is Practice Jwt Sign Up Form"}),Object(d.jsxs)("div",{className:"form-floating",children:[Object(d.jsx)("input",Object(l.a)(Object(l.a)({ref:O},g("fullname",{maxLength:15,minLength:3})),{},{className:"form-control",id:"floatingPassword",placeholder:"Name",onChange:function(e){s(Object(l.a)(Object(l.a)({},a),{},{name:e.target.value})),console.log(a)}})),Object(d.jsx)("label",{for:"floatingPassword",children:"Name"})]}),Object(d.jsxs)("div",{className:"form-floating",children:[Object(d.jsx)("input",Object(l.a)(Object(l.a)({type:"email"},g("email",{pattern:{value:"^([a-z]){1,}@([a-z]){3,8}(.)([a-z]){3,5}$",message:"email"}})),{},{className:"form-control",id:"floatingInput",placeholder:"name@example.com",onChange:function(e){s(Object(l.a)(Object(l.a)({},a),{},{email:e.target.value})),console.log(a)}})),Object(d.jsx)("label",{for:"floatingInput",children:"Email address"})]}),Object(d.jsxs)("div",{className:"form-floating",children:[Object(d.jsx)("input",Object(l.a)(Object(l.a)({type:"password"},g("password",{required:!0,pattern:"^[a-zA-z]+$/i"})),{},{className:"form-control",id:"floatingPassword",placeholder:"Password",onChange:function(e){s(Object(l.a)(Object(l.a)({},a),{},{password:e.target.value})),console.log(a)}})),Object(d.jsx)("label",{for:"floatingPassword",children:"Password"})]}),Object(d.jsx)("button",{onClick:p,className:"w-100 btn btn-lg btn-primary",type:"submit",children:"Sign in"}),Object(d.jsx)("p",{className:"mt-5 mb-3 text-muted",children:"\xa9 2017\u20132021"})]})})})};function f(){var e=Object(m.f)(),t=!1;j.a.interceptors.response.use((function(t){return 401===t.status&&e("/login"),t}));var a=Object(n.useState)({name:"",email:""}),s=Object(r.a)(a,2),c=s[0],i=s[1];return Object(n.useEffect)((function(){try{j.a.get("https://jwtauthfrontend.azurewebsites.net/api/auth/user",{withCredentials:!0,headers:{"Access-Control-Allow-Origin":"https://jwtauthfrontend.azurewebsites.net","Access-Control-Allow-Credentials":!0}}).catch((function(a){console.log("no worky"),t=!0,e("/login")})).then((function(e){0==t&&i(Object(l.a)({},e.data))}))}catch(a){e("/login")}})),Object(d.jsxs)("div",{children:["Welcome Back ",c.name]})}a(54);var g=a(9);function p(e){var t;return t=""===e.name?Object(d.jsx)(d.Fragment,{children:Object(d.jsxs)("ul",{className:"navbar-nav me-auto mb-2 mb-md-0",children:[Object(d.jsx)("li",{className:"nav-item",children:Object(d.jsx)(g.b,{className:"nav-link active",to:"/Login",children:"Login"})}),Object(d.jsx)("li",{className:"nav-item",children:Object(d.jsx)(g.b,{className:"nav-link active",to:"/Register",children:"Register"})})]})}):Object(d.jsx)(d.Fragment,{children:Object(d.jsxs)("ul",{className:"navbar-nav me-auto mb-2 mb-md-0",children:[Object(d.jsx)("li",{className:"nav-item",children:Object(d.jsx)(g.b,{className:"nav-link active",to:"/",onClick:function(){j.a.post("https://jwtauthfrontend.azurewebsites.net/api/auth/logout",{},{withCredentials:!0,headers:{"Access-Control-Allow-Origin":"https://jwtauthfrontend.azurewebsites.net","Access-Control-Allow-Credentials":!0}}).then((function(e){console.log(e.data)})).catch((function(e){}))},children:"Logout"})}),Object(d.jsx)("li",{className:"nav-item",children:Object(d.jsx)(g.b,{className:"nav-link active",to:"/Register",children:"Register"})})]})}),Object(d.jsx)(d.Fragment,{children:Object(d.jsx)("nav",{className:"navbar navbar-expand-md navbar-dark bg-dark mb-4",children:Object(d.jsxs)("div",{className:"container-fluid",children:[Object(d.jsx)(g.b,{to:"/",className:"navbar-brand",children:"Home"}),Object(d.jsx)("div",{children:t})]})})})}var x=Object(b.a)((function(){var e=Object(n.useState)(""),t=Object(r.a)(e,2),a=t[0],s=t[1];return Object(n.useEffect)((function(){j.a.get("https://jwtauthfrontend.azurewebsites.net/api/auth/user",{headers:{"Access-Control-Allow-Origin":"https://jwtauthfrontend.azurewebsites.net","Access-Control-Allow-Credentials":!0}}).then((function(e){return s(e.data.name)}))})),Object(d.jsx)(d.Fragment,{children:Object(d.jsxs)("div",{className:"App",children:[Object(d.jsx)(p,{name:a,setName:s}),Object(d.jsx)("main",{className:"form-signin",children:Object(d.jsxs)(m.c,{children:[Object(d.jsx)(m.a,{path:"/",element:Object(d.jsx)(f,{name:a})}),Object(d.jsx)(m.a,{path:"/Login",element:Object(d.jsx)(u,{setName:s})}),Object(d.jsx)(m.a,{path:"/Register",element:Object(d.jsx)(O,{})})]})})]})})})),w=function(e){e&&e instanceof Function&&a.e(3).then(a.bind(null,56)).then((function(t){var a=t.getCLS,n=t.getFID,s=t.getFCP,c=t.getLCP,r=t.getTTFB;a(e),n(e),s(e),c(e),r(e)}))},v=document.getElementsByTagName("base")[0].getAttribute("href"),N=document.getElementById("root");c.a.render(Object(d.jsx)(g.a,{basename:v,children:Object(d.jsx)(x,{})}),N),w()}},[[55,1,2]]]);
//# sourceMappingURL=main.9b6a18aa.chunk.js.map