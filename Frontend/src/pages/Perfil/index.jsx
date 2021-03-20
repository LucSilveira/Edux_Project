import React, { useState, useEffect } from 'react';
import { Form, Button, Nav} from 'react-bootstrap'
import Menu from '../../components/menu';
import Footer from '../../components/footer';
import { url } from '../../utils/constants'
import jwt_decode from 'jwt-decode'
import {Link} from 'react-router-dom'
import './style.css'

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUserAltSlash, faPencilAlt } from '@fortawesome/free-solid-svg-icons'


const Perfil = () => {
    const [nome, setNome] = useState('')
    const [email, setEmail] = useState('')
    const [foto, setFoto] = useState('')
    const [idUsuario, setIdUsuario] = useState(0)
    const [turmas, setTurmas] = useState([])

    useEffect(() => {
        BuscarUsuario();
        VerificarTurma()
    }, [])

    const passIdTurma = (event) => {
        event.preventDefault()
        // console.log(event.target.value)
        localStorage.setItem('item', event.target.value)
    }

    const VerificarTurma = () => {
        const perfilUsuario = jwt_decode(localStorage.getItem('token-edux')).Role;
        const idDoUsuario = jwt_decode(localStorage.getItem('token-edux')).IdUsuario;

        const urlRequest = (perfilUsuario === 'Aluno' ? url + '/alunoturma/aluno/' + idDoUsuario : url + '/professorturma/professor/' + idDoUsuario)

        fetch(urlRequest, {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            setTurmas(data)
        })
        .catch(error => console.log(error))
    }

    const BuscarUsuario = () => {
        fetch(url + '/usuario/' + jwt_decode(localStorage.getItem('token-edux')).IdUsuario, {
            method : 'GET'
        })
        .then(response => response.json())
        .then(data => {
            setNome(data.nome)
            setEmail(data.email)
            setFoto(data.imagem)
        })
        .catch(error => console.log(error))
    }

    const AlterarDados = (event) => {
        event.preventDefault();
        
        const idDoUsuario = jwt_decode(localStorage.getItem('token-edux')).IdUsuario;
        let professor = new FormData();
        professor.set('Nome', nome);
        professor.set('Email', email);
        professor.append('Arquivo', foto)

        fetch(url + '/usuario/' + idDoUsuario, {
            method : 'PUT',
            body : professor
        })
        .then(response => response.json())
        .then(data => BuscarUsuario())
        .catch(error => console.log(error))
    }

    return (
        <div>
            <Menu />
            <section className="content-perfil">
                <section className="dados-usuario">
                    {
                        foto === 'padrao.jpg' ? 
                        <>
                            <FontAwesomeIcon icon={faUserAltSlash} size="10x" color="#000"/>
                            <p className="errorFoto">Você ainda não informou uma foto</p>
                            <Form.Group>
                                <Form.File id='fileUsuario' onChange={e => setFoto(e.target.files[0])}/>
                            </Form.Group>
                        </>
                        : 
                        <div className="fotoUsuario" style={{ backgroundImage : `url(${foto})`}} /> 
                    }
                    <Form.Group controlId="formBasicNome" className="nomeUsuario">
                        <Form.Control value={nome} onChange={e => setNome(e.target.value)} type="text" placeholder={nome} />
                    </Form.Group>
                    <div className="alt-data">
                        <Form.Group controlId="formBasicEmail">
                            <Form.Control value={email} onChange={e => setEmail(e.target.value)} type="email" placeholder={email} />
                        </Form.Group>
                        <button onClick={e => AlterarDados(e)}><FontAwesomeIcon icon={faPencilAlt} size="2x" color="#000"/></button>
                    </div>
                </section>
                <section className="turmas-content">
                    <div className="link-titulo">
                        <h2>Confira as suas turmas</h2>
                        <a href="/turmas">ver todas as turmas</a>
                    </div>
                    {
                        turmas.map((turma, index) => {
                            return (
                                <div className="turma-item" key={index}>
                                    <div className="turma-item-descricao">
                                        <p className="titulo">{turma.idTurmaNavigation.idCursoNavigation.titulo}</p>
                                        <p className="descricao">{turma.idTurmaNavigation.descricao}</p>
                                    </div>
                                    <Button variant="info"><Link to={{ pathname : '/turma/especifica', state : {IdTurma : turma.id} }}>Ver detalhes</Link></Button>
                                </div>
                            )
                        })
                    }
                </section>
            </section>
            <Footer />
        </div>
    )
}

export default Perfil;