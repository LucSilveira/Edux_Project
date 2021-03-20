import React, { useState } from 'react'
import Menu from '../../components/menu'
import Rodape from '../../components/footer'
import { useEffect } from 'react';
import { url } from '../../utils/constants'
import './style.css';

const TurmaEspecifica = (props) => {
    const [idTurma, setIdTurma] = useState(props.location.state.IdTurma)
    const [objetivos, setObjetivos] = useState([])
    const [professores, setProfessores] = useState([])
    const [alunos, setAlunos] = useState([])
    const [descricao, setDescricao] = useState('')
    const [titulo, setTitulo] = useState('')

    useEffect(() => {
        ListarObjetivos()
        BuscarInformacoes()
        ListarProfessores()
        ListarAlunos()
    }, [])

    const ListarObjetivos = () => {
        fetch(url + '/objetivo', {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => setObjetivos(data))
        .catch(error => console.log(error))
    }

    const ListarProfessores = () => {
        fetch(url + '/professorturma/turma/' + idTurma, {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => setProfessores(data))
        .catch(error => console.log(error))
    }

    const ListarAlunos = () => {
        fetch(url + '/alunoturma/turma/' + idTurma, {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => setAlunos(data))
        .catch(error => console.log(error))
    }

    const BuscarInformacoes = () => {
        fetch(url + '/turma/' + idTurma, {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            setDescricao(data.descricao)
            setTitulo(data.idCursoNavigation.titulo)
        })
        .catch(error => console.log(error))
    }

    return (
        <>
            <Menu />
            <section className="content-turma">
                <section className="turma-listas">
                    <div className="dados-cursos">
                        <div className="turma-info">
                            <p className="turma-titulo">{titulo}</p>
                            <p className="turma-descricao">{descricao}</p>
                        </div>
                        <div className="turma-proffs">
                            <h5>Professores</h5>
                            {
                                professores.map((professor, index) => {
                                    return (
                                        <p key={index}>{professor.idUsuarioNavigation.nome}</p>
                                    )
                                })
                            }
                        </div>
                    </div>
                    <div className="list-alunos">
                        <h5>Lista dos alunos</h5>
                        {
                            alunos.map((aluno, index) => {
                                return (
                                    <div className="content-aluno" key={index}>
                                        <p className="aluno-nome">{aluno.idUsuarioNavigation.nome}</p>
                                        <p className="aluno-email">{aluno.idUsuarioNavigation.email}</p>
                                    </div>
                                )
                            })
                        }
                    </div>
                </section>
                <section className="objetivos-list">
                    <h3>Objetivos do curso</h3>
                    {
                        objetivos.map((objetivo, index) => {
                            return (
                                <div className="obj-item" key={index} >
                                    <h5>{objetivo.titulo}</h5>
                                    <p>{objetivo.descricao}</p>
                                </div>
                            )
                        })
                    }
                </section>
            </section>
            <Rodape />
        </>
    )
}

export default TurmaEspecifica