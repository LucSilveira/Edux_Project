import React from 'react';
import LogoEDUX from '../../assets/imagens/LogoEDUX.png';
import {Navbar, Nav, NavDropdown} from 'react-bootstrap';
import './index.css';
import jwt_decode from 'jwt-decode'
import { useHistory } from 'react-router-dom'


const Menu = () => {
    const history = useHistory()

    const logout = (event) =>{
        event.preventDefault();

        localStorage.removeItem('token-edux');
        history.push('/')
    }

    const renderMenu = () => {
        const token = localStorage.getItem('token-edux');

        if(token == null){
            return (
                <Nav>
                    <Nav.Link href="/login">Login</Nav.Link>
                    <Nav.Link href="/cadastrar">Cadastrar</Nav.Link>
                </Nav>
            )
        }else if(jwt_decode(token).Role === "Administrador"){
            return (
                <Nav>
                    <Nav.Link href="/admin/dashboard">Dashboard</Nav.Link>
                    <Nav.Link href="/perfil">Perfil</Nav.Link>
                    <Nav.Link href="/turmas">Turmas</Nav.Link>
                    <Nav.Link href="/login" onClick={e => logout(e)}>Sair</Nav.Link>
                </Nav>
            )
        }else{
            return (
                <Nav>
                    <Nav.Link href="/perfil">Perfil</Nav.Link>
                    <Nav.Link href="/turmas">Turmas</Nav.Link>
                    <Nav.Link href="/login" onClick={e => logout(e)}>Sair</Nav.Link>
                </Nav>
            )
        }
    }

    return(
        <div className="dspf">

        <div className="lgm">
            <img src={LogoEDUX} />
        </div>

            <div className="nvb">
                {
                    renderMenu()
                }
            </div>
        </div>
    )
}

export default Menu;