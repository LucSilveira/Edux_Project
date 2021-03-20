import React, { useEffect, useState } from 'react'
import Menu from '../../components/menu'
import Rodape from '../../components/footer'
import { Card, Button, Form } from 'react-bootstrap'
import './style.css'
import { url } from '../../utils/constants'
import jwt_decode from 'jwt-decode'

const Turmas = () => {
    const [turmas, setTurmas] = useState([])
    const [cursos, setCursos] = useState([])
    const [descricao, setDescricao] = useState('')
    const [idCurso, setIdCurso] = useState(0)
    const [id, setId] = useState(0)
    const [status, setStatus] = useState(false)
    const perfilUsuario = jwt_decode(localStorage.getItem('token-edux')).Role;

    useEffect(() => {
        ListarTurmas()
        ListarCursos()
    }, [])

    const ListarTurmas = () => {
        fetch(url + '/turma', {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => setTurmas(data))
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

    const Excluir = (event) => {
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

    const InserirTurmaProfessor = (event) => {
        event.preventDefault()

        let professorTurma = {
            'matricula' : 'abc123',
            'idUsuario' : jwt_decode(localStorage.getItem('token-edux')).IdUsuario,
            'idTurma' : event.target.value
        }

        fetch(url + '/professorturma', {
            method : 'POST',
            body : JSON.stringify(professorTurma),
            headers : {
                'content-type' : 'application/json',
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarTurmas())
        .catch(error => console.log(error))
    }

    const InserirTurmaAluno = (event) => {
        event.preventDefault()
        
        let alunoTurma = {
            'matricula' : 'abc123',
            'idUsuario' : jwt_decode(localStorage.getItem('token-edux')).IdUsuario,
            'idTurma' : event.target.value
        }

        fetch(url + '/alunoturma', {
            method : 'POST',
            body : JSON.stringify(alunoTurma),
            headers : {
                'content-type' : 'application/json',
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarTurmas())
        .catch(error => console.log(error))
    }

    const progressionContent = () => {
        if(status == false){
            return (<></>)
        }
        else{
            return (
                <section className="add-turma">
                    <h4>Cadastrar uma nova turma</h4>
                    <hr />
                    <Form className="formulario">
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
                        <div className="buttons-form">
                            <Button type="submit" onClick={e => setStatus(false) | Cadastrar(e)} variant="success">Salvar</Button>
                            <Button variant="secondary" style={{ marginLeft : '10px'}} onClick={e => setStatus(false)}>Cancelar</Button>
                        </div>
                    </Form>
                </section>
            )
        }
    }

    return (
        <>
            <Menu />
            <section className="content-turmas">
                <div className="title-pag">
                    <h2>Confira todas as turmas do nosso sistema!</h2>
                    <p onClick={e => setStatus(true)}>adicionar turma</p>
                </div>
                <section className="crud-turmas">
                    {
                        perfilUsuario === 'Aluno' ? <></>
                        : progressionContent()

                    }
                    <section className="turma-list">
                        {
                            turmas.map((turma, index) => {
                                return (
                                    <Card className="turma-item" key={index}>
                                        <Card.Body>
                                            <Card.Title>{turma.idCursoNavigation.titulo}</Card.Title>
                                            <Card.Text>
                                            {turma.descricao}
                                            </Card.Text>
                                            {
                                                perfilUsuario === 'Aluno' ? 
                                                <Button value={turma.id} onClick={e => InserirTurmaAluno(e)}  variant="info">Inserir na turma</Button>
                                                : <div className="buttons">
                                                    <div style={{marginTop : '20px'}}>
                                                        <Button value={turma.id} onClick={e => setStatus(true) | BuscarInformacoes(e)} variant="warning" style={{marginRight : '10px'}}>Editar</Button>
                                                        <Button value={turma.id} onClick={e => Excluir(e)} variant="danger">Excluir</Button>
                                                    </div>
                                                    <div className="inserir-usuario">
                                                        <Button value={turma.id} onClick={e => InserirTurmaProfessor(e)}  variant="info">Lecionar turma</Button>
                                                    </div>
                                                </div>                                                
                                            }
                                        </Card.Body>
                                    </Card>
                                )
                            })
                        }
                    </section>
                </section>
            </section>
            <Rodape />
        </>
    )
}

export default Turmas