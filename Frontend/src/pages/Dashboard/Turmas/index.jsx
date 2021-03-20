import React, { useEffect, useState } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import './style.css'
import { url } from '../../../utils/constants'


const TurmaCrud = () => {
    const [turmas, setTurmas] = useState([]);
    const [descricao, setDescricao] = useState('');
    const [idCurso, setIdCurso] = useState(0);
    const [cursos, setCursos] = useState([])
    const [id, setId] = useState(0);

    useEffect(() => {
        ListarTurmas();
        ListarCursos();
    }, [])

    const ListarTurmas = () => {
        fetch(url + '/turma', {
            method : 'GET',
            headers : {
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            setTurmas(data)

            //limpando campos
            setDescricao('')
            setIdCurso(0)
        })
        .catch(error => console.log(error))
    }

    const ListarCursos = () => {
        fetch(url + '/curso', {
            method : 'GET',
            headers : {
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            setCursos(data)
        })
        .catch(error => console.log(error))

        setDescricao('');
        setIdCurso(0)
    }

    const BuscarInformacoes = (event) => {
        event.preventDefault();

        fetch(url + '/turma/' + event.target.value, {
            method : 'GET',
            headers : {
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            setId(data.id)
            setDescricao(data.descricao);
            setIdCurso(data.idCurso);
        })
    }

    const Cadastrar = (event) => {
        event.preventDefault();
        
        let method = (id === 0 ? 'POST' : 'PUT');
        let urlRequest = (id === 0 ? url + '/turma' : url + '/turma/' + id);

        fetch(urlRequest, {
            method : method,
            body : JSON.stringify({
                'id' : id,
                'descricao' : descricao,
                'idCurso' : parseInt(idCurso)
            }),
            headers : {
                'content-type' : 'application/json',
                'authorization' :  'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarTurmas())
        .catch(error => console.log(error))
    }

    const ExcluirTurma = (event) => {
        event.preventDefault()

        fetch(url + '/turma/' + event.target.value, {
            method : 'DELETE',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarTurmas())
        .catch(error => console.log(error))
    }

    return (
        <>
            <section className="content-crud">
                <h1>Gerencie os dados dos turmas!</h1>
                <Form className="form-turmas" onSubmit={e => Cadastrar(e)}>
                    <Form.Group controlId="formBasicTitulo">
                        <Form.Label>Descricao</Form.Label>
                        <Form.Control as="textarea" rows={2} type="text" value={descricao} onChange={e => setDescricao(e.target.value)} placeholder="Informe o titulo do titulo ..." />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Curso pertencente</Form.Label>
                        <Form.Control as="select" custom defaultValue={idCurso} onChange={e => setIdCurso(e.target.value)}>
                        <option value={0}>Selecione uma opção</option>
                        {
                            cursos.map((curso, index) => {
                                return(
                                    <option key={index} value={curso.id}>{curso.titulo}</option>
                                )
                            })
                        }
                        </Form.Control>
                    </Form.Group>
                    <Button type="submit" variant="success">Salvar</Button>
                </Form>
                <Table className="table-turmas">
                    <thead>
                        <tr>
                            <th>Identificação</th>
                            <th>Titulo</th>
                            <th>Descrição</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            turmas.map((turma, index) => {
                                return (
                                    <tr key={index}>
                                        <td>{turma.id}</td>
                                        <td>{turma.idCursoNavigation.titulo}</td>
                                        <td>{turma.descricao}</td>
                                        <td>
                                            <Button style={{ marginRight : '1em'}} value={turma.id} onClick={e => BuscarInformacoes(e)} variant="warning">Alterar</Button>
                                            <Button value={turma.id} onClick={e => ExcluirTurma(e)} variant="danger">Remover</Button>
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

export default TurmaCrud