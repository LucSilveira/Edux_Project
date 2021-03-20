import React, { useEffect, useState } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import './style.css'
import { url } from '../../../utils/constants'


const CursoCrud = () => {
    const [cursos, setCursos] = useState([]);
    const [titulo, setTitulo] = useState('');
    const [idInstituicao, setIdInstituicao] = useState(0);
    const [instituicoes, setInstituicoes] = useState([])
    const [id, setId] = useState(0);

    useEffect(() => {
        ListarCursos();
        ListarInstituicoes();
    }, [])

    const ListarCursos = () => {
        fetch(url + '/curso', {
            method : 'GET',
            headers : {
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => setCursos(data))
        .catch(error => console.log(error))
    }

    const ListarInstituicoes = () => {
        fetch(url + '/instituicao', {
            method : 'GET',
            headers : {
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            setInstituicoes(data)

            //limpando campos
            setTitulo('')
            setIdInstituicao(0)
        })
        .catch(error => console.log(error))
    }

    const BuscarInformacoes = (event) => {
        event.preventDefault();

        fetch(url + '/curso/' + event.target.value, {
            method : 'GET',
            headers : {
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            setId(data.id)
            setTitulo(data.titulo);
            setIdInstituicao(data.idInstituicao);
        })
    }

    const Cadastrar = (event) => {
        event.preventDefault();
        
        let method = (id === 0 ? 'POST' : 'PUT');
        let urlRequest = (id === 0 ? url + '/curso' : url + '/curso/' + id);

        fetch(urlRequest, {
            method : method,
            body : JSON.stringify({
                'id' : id,
                'titulo' : titulo,
                'idInstituicao' : parseInt(idInstituicao)
            }),
            headers : {
                'content-type' : 'application/json',
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarCursos())
        .catch(error => console.log(error))
    }

    const ExcluirCurso = (event) => {
        event.preventDefault()

        fetch(url + '/curso/' + event.target.value, {
            method : 'DELETE',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarCursos())
        .catch(error => console.log(error))
    }

    return (
        <>
            <section className="content-crud">
                <h1>Gerencie os dados dos cursos!</h1>
                <Form className="form-cursos" onSubmit={e => Cadastrar(e)}>
                    <Form.Group controlId="formBasicTitulo">
                        <Form.Label>Titulo</Form.Label>
                        <Form.Control type="text" value={titulo} onChange={e => setTitulo(e.target.value)} placeholder="Informe o titulo do curso ..." />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Instituição pertencente</Form.Label>
                        <Form.Control as="select" size="md" custom defaultValue={idInstituicao} onChange={e => setIdInstituicao(e.target.value)}>
                        <option value={0}>Selecione uma opção</option>
                        {
                            instituicoes.map((instituicao, index) => {
                                return(
                                    <option key={index} value={instituicao.id}>{instituicao.nome}</option>
                                )
                            })
                        }
                        </Form.Control>
                    </Form.Group>
                    <Button type="submit" variant="success">Salvar</Button>
                </Form>
                <Table className="table-cursos">
                    <thead>
                        <tr>
                            <th>Identificação</th>
                            <th>Nome</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            cursos.map((curso, index) => {
                                return (
                                    <tr key={index}>
                                        <td>{curso.id}</td>
                                        <td>{curso.titulo}</td>
                                        <td>
                                            <Button style={{ marginRight : '1em'}} value={curso.id} onClick={e => BuscarInformacoes(e)} variant="warning">Alterar</Button>
                                            <Button value={curso.id} onClick={e => ExcluirCurso(e)} variant="danger">Remover</Button>
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

export default CursoCrud