import React, { useEffect, useState } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import './style.css'
import { url } from '../../../utils/constants'


const ProfessorCrud = () => {
    const [professores, setProfessores] = useState([]);
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [id, setId] = useState(0);

    useEffect(() => {
        ListarProfessores();
    }, [])

    const ListarProfessores = () => {
        fetch(url + '/usuario/professor', {
            method : 'GET'
        })
        .then(response => response.json())
        .then(data => setProfessores(data))
        .catch(error => console.log(error))
    }

    const BuscarInformacoes = (event) => {
        event.preventDefault();

        fetch(url + '/usuario/' + event.target.value, {
            method : 'GET'
        })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            setId(data.id)
            setNome(data.nome);
            setEmail(data.email);
        })
    }

    const Cadastrar = (event) => {
        event.preventDefault();

        let professor = {
            'id' : id,
            'Nome' : nome,
            'Email' : email,
            'Senha' : senha
        }

        fetch(url + '/usuario', {
            method : 'POST',
            body : JSON.stringify(professor),
            headers : {
                'content-type' : 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            ListarProfessores()
            console.log(data)
        })
        .catch(error => console.log(error))

        LimparCampos()
    }

    const AlterarDados = (event) => {
        event.preventDefault();

        let professor = new FormData();
        professor.set('Nome', nome);
        professor.set('Email', email);
        professor.set('Senha', senha)

        fetch(url + '/usuario/' + id, {
            method : 'PUT',
            body : professor
        })
        .then(response => response.json())
        .then(data => {
            ListarProfessores()
            console.log(data)
        })
        .catch(error => console.log(error))

        LimparCampos();
    } 

    const ExcluirProfessor = (event) => {
        event.preventDefault()

        fetch(url + '/usuario/' + event.target.value, {
            method : 'DELETE'
        })
        .then(response => response.json())
        .then(data => ListarProfessores())
        .catch(error => console.log(error))
    }

    const LimparCampos = () => {
        setEmail('');
        setSenha('');
        setNome('');
    }

    return (
        <>
            <section className="content-crud">
                <h1>Gerencie os dados dos professores!</h1>
                <Form className="form-professores" onSubmit={e => Cadastrar(e)}>
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
                    <Button type="submit" onClick={e => AlterarDados(e)} variant="warning">Salvar dados</Button>
                </Form>
                <Table className="table-professores">
                    <thead>
                        <tr>
                            <th>Identificação</th>
                            <th>Professores</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            professores.map((professor, index) => {
                                return (
                                    <tr key={index}>
                                        <td>{professor.id}</td>
                                        <td>
                                            {professor.nome}
                                        </td>
                                        <td>
                                            <Button value={professor.id} onClick={e => BuscarInformacoes(e)} variant="warning">Alterar</Button>
                                            <Button value={professor.id} onClick={e => ExcluirProfessor(e)} variant="danger">Remover</Button>
                                        </td>
                                    </tr>
                                )
                            })
                        }
                    </tbody>
                </Table>
            </section>
        </>
    )
}

export default ProfessorCrud