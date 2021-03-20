import React from 'react'
import Menu from '../../components/menu'
import Rodape from '../../components/footer'
import { Button, Container, Form } from 'react-bootstrap'
import { useState } from 'react'
import { url } from '../../utils/constants'
import { useHistory } from 'react-router-dom'
import './index.css'

const Cadastrar = () => {
    const history = useHistory()
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');

    const CadastrarAluno = (event) => {
        event.preventDefault();

        fetch(url + '/usuario', {
            method : 'POST',
            body : JSON.stringify({
                Nome : nome,
                Email : email,
                Senha : senha
            }), headers : {
                'content-type' : 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => history.push('/login'))
        .catch(error => console.log(error))
    }
 
    return (
        <>
            <Menu />
            <Container className='form-height'>
                <Form className='form-signin'  onSubmit={e => CadastrarAluno(e)}>
                    <div className='text-center'> 
                    </div>
                    <br/>
                    <small>Informe os dados Abaixo</small>
                    <hr/>
                    <Form.Group controlId="formBasicNome">
                        <Form.Label>Nome Completo</Form.Label>
                        <Form.Control type="text" value={nome} onChange={e => setNome(e.target.value)} placeholder="Informe o nome completo ..." />
                    </Form.Group>
                    <Form.Group controlId="formBasicEmail">
                        <Form.Label>Email</Form.Label>
                        <Form.Control type="email" value={email} onChange={e => setEmail(e.target.value)} placeholder="Informe o email ..." />
                    </Form.Group>
                    <Form.Group controlId="formBasicSenha">
                        <Form.Label>Senha</Form.Label>
                        <Form.Control type="password" value={senha} onChange={e => setSenha(e.target.value)} placeholder="Informe a senha ..." />
                    </Form.Group>
                    <Button type="submit" variant="success">Cadastrar</Button>
                </Form>
            </Container>
            <Rodape />
        </>
    )
}

export default Cadastrar