import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import Home from './pages/Home';
import Login from './pages/login';
import Cursos from './pages/cursos';
import Dashboard from './pages/Dashboard'
import Perfil from './pages/Perfil'
import Turmas from './pages/Turmas'
import TurmaEspecifica from './pages/TurmaEspecifica'
import Cadastrar from './pages/cadastrar'
import jwt_decode from 'jwt-decode';

import { BrowserRouter as Router, Route, Redirect, Link, Switch } from "react-router-dom";

const RotaPrivada = ({component : Component, ...rest}) => (
  <Route
    {...rest}
    render = { props => 
        localStorage.getItem('token-edux') !== null ?
          (<Component {...props}/>) : 
          (<Redirect to={{pathname : '/login', state : {from : props.location}}}/>) 
    }
  />
) ;

const RotaPrivadaAdmin = ({component : Component, ...rest}) => (
  <Route
    {...rest}
    render = { props => 
        localStorage.getItem('token-edux') !== null && jwt_decode(localStorage.getItem('token-edux')).Role === 'Administrador' ?
          (<Component {...props}/>) : 
          (<Redirect to={{pathname : '/login', state : {from : props.location}}, localStorage.removeItem('token')}/>)
    }
  />
);

// Usado para definir as rotas da aplicação
const routing = (
  <Router>
    <div>
      <Switch>
        <Route exact path='/' component={Home} />
        <Route path='/login' component={Login} />
        <Route path='/cadastrar' component={Cadastrar} />
        <RotaPrivada path='/cursos' component={Cursos} />
        <RotaPrivada path='/perfil' component={Perfil} />
        <RotaPrivada path='/turmas' component={Turmas} />
        <RotaPrivada path='/turma/especifica' component={TurmaEspecifica} />
        <RotaPrivadaAdmin path='/admin/dashboard' component={Dashboard} />

    </Switch>
  </div>
  </Router>
  )
  
  ReactDOM.render(
  routing,
  document.getElementById('root')
  );

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

