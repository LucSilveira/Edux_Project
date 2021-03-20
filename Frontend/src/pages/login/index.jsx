import React, { useState } from 'react';
import { useHistory } from "react-router-dom";
import jwt_decode from 'jwt-decode';
import Menu from '../../components/menu';
import Footer from '../../components/footer';
import {url} from '../../utils/constants';
import {Form, Container, Button} from 'react-bootstrap';
import './index.css';

const Login = () => {
    let history = useHistory();

    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');

    const logar = (event) => {
        event.preventDefault();

        console.log(`${email} - ${senha}`);

        fetch(url + '/login',{
            method : 'POST',
            body : JSON.stringify({
                Email : email,
                Senha : senha
            }),
            headers : {
                'content-type' : 'application/json'
            }
        })
 
        .then(response => {
            //verifica a resposta da api
            if(response.ok === true){
                return response.json();
            }
            //caso n retorne ok manda o alerta
            alert('Dados invalidos')
    })
        .then(data => {

            //armazena o token no local storage
            localStorage.setItem('token-edux', data.token);

            let usuario = jwt_decode(data.token);

            if(usuario.role === 'Admin'){
                history.push('/admin/dashboard');
            } else{
                history.push("/cursos");
            }            
        })
        .catch(err => console.error(err))
}
    return(
        <div>
            <Menu/>
            <Container className='form-height'>
            <Form className='form-signin' onSubmit={event => logar(event)}> 
            <div className='text-center'>
                <img src></img>
                </div>
                <br/>
                <small>Informe os dados abaixo</small>
                <hr/>
            <Form.Group controlId="formBasicEmail">
                <Form.Label>Email</Form.Label>
                <Form.Control type="email" placeholder="Informe o email" value={email} onChange={event => setEmail(event.target.value)} required />
                <Form.Text className="text-muted">
                </Form.Text>
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
                <Form.Label>Senha</Form.Label>
                <Form.Control type="senha" placeholder="Informe sua senha" value={senha} onChange={event => setSenha(event.target.value)} required />
            </Form.Group>
            <Button variant="primary" type="submit">
                Enviar
            </Button>
            <br/><br/>
            <a href='/cadastrar' style={{marginTop:'30px'}}>Não possuo conta</a>
            </Form>
            </Container>
            <div style={{marginBottom: '50px'}}></div>
            <Footer />
        </div>
    )
}
export default Login;