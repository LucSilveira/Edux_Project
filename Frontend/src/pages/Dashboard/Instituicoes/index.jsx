import React, { useEffect, useState } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import './style.css'
import { url } from '../../../utils/constants'


const InstituicaoCrud = () => {
    const [instituicoes, setInstituicoes] = useState([]);
    const [nome, setNome] = useState('');
    const [logradouro, setLogradouro] = useState('');
    const [numero, setNumero] = useState('');
    const [complemento, setComplemento] = useState('');
    const [bairro, setBairro] = useState('');
    const [cidade, setCidade] = useState('');
    const [cep, setCep] = useState('');
    const [uf, setUf] = useState('');
    const [id, setId] = useState(0);

    useEffect(() => {
        ListarInstituicoes();
    }, [])

    const ListarInstituicoes = () => {
        fetch(url + '/instituicao', {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => setInstituicoes(data))
        .catch(error => console.log(error))
    }

    const BuscarInformacoes = (event) => {
        event.preventDefault();

        fetch(url + '/instituicao/' + event.target.value, {
            method : 'GET',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            setId(data.id)
            setNome(data.nome);
            setLogradouro(data.logradouro);
            setNumero(data.numero);
            setComplemento(data.complemento);
            setBairro(data.bairro);
            setCidade(data.cidade);
            setUf(data.uf);
            setCep(data.cep);
        })
    }

    const Cadastrar = (event) => {
        event.preventDefault();

        let professor = {
            'id' : id,
            'Nome' : nome,
            'Logradouro' : logradouro,
            'Numero' : numero,
            'Complemento' : complemento,
            'Bairro' : bairro,
            'Cidade' : cidade,
            'Uf' : uf,
            'Cep' : cep
        }

        let method = (id === 0 ? 'POST' : 'PUT');
        let urlRequest = (id === 0 ? url + '/instituicao' : url + '/instituicao/' + id);

        fetch(urlRequest, {
            method : method,
            body : JSON.stringify(professor),
            headers : {
                'content-type' : 'application/json',
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => {
            ListarInstituicoes()
            console.log(data)
        })
        .catch(error => console.log(error))

        LimparCampos()
    }

    const ExcluirProfessor = (event) => {
        event.preventDefault()

        fetch(url + '/instituicao/' + event.target.value, {
            method : 'DELETE',
            headers : {
                'authorization' : 'Bearer ' + localStorage.getItem('token-edux')
            }
        })
        .then(response => response.json())
        .then(data => ListarInstituicoes())
        .catch(error => console.log(error))
    }

    const LimparCampos = () => {
        setNome('');
        setLogradouro('');
        setNumero('');
        setComplemento('');
        setBairro('');
        setCidade('');
        setUf('');
        setCep('');
        setId(0);
    }

    return (
        <>
            <section className="content-crud">
                <h1>Gerencie os dados dos professores!</h1>
                <Form className="form-instituicao" onSubmit={e => Cadastrar(e)}>
                    <div className="firstPartForm">
                        <Form.Group controlId="formBasicNome">
                            <Form.Label>Nome Insituicao</Form.Label>
                            <Form.Control type="text" value={nome} onChange={e => setNome(e.target.value)} placeholder="Informe o nome ..." />
                        </Form.Group>
                        <Form.Group controlId="formBasicLogradouro">
                            <Form.Label>Logradouro</Form.Label>
                            <Form.Control type="text" value={logradouro} onChange={e => setLogradouro(e.target.value)} placeholder="Informe o logradouro ..." />
                        </Form.Group>
                        <Form.Group controlId="formBasicNumero">
                            <Form.Label>Numero</Form.Label>
                            <Form.Control type="text" value={numero} onChange={e => setNumero(e.target.value)} placeholder="Informe a numero ..." />
                        </Form.Group>
                    </div>
                    <div className="secondPartForm">
                        <Form.Group controlId="formBasicComplemento">
                            <Form.Label>Complemento</Form.Label>
                            <Form.Control type="text" value={complemento} onChange={e => setComplemento(e.target.value)} placeholder="Informe o complemento ..." />
                        </Form.Group>
                        <Form.Group controlId="formBasicBairro">
                            <Form.Label>Bairro</Form.Label>
                            <Form.Control type="text" value={bairro} onChange={e => setBairro(e.target.value)} placeholder="Informe o bairro ..." />
                        </Form.Group>
                        <Form.Group controlId="formBasicCidade">
                            <Form.Label>Cidade</Form.Label>
                            <Form.Control type="text" value={cidade} onChange={e => setCidade(e.target.value)} placeholder="Informe o cidade ..." />
                        </Form.Group>
                    </div>
                    <div className="thirdPartForm">
                        <Form.Group controlId="formBasicUf">
                            <Form.Label>Uf</Form.Label>
                            <Form.Control type="text" value={uf} onChange={e => setUf(e.target.value)} placeholder="Informe o uf ..." />
                        </Form.Group>
                        <Form.Group controlId="formBasicCep">
                            <Form.Label>Cep</Form.Label>
                            <Form.Control type="text" value={cep} onChange={e => setCep(e.target.value)} placeholder="Informe o CEP ..." />
                        </Form.Group>
                        <Button type="submit" variant="success">Cadastrar</Button>
                    </div>
                </Form>
                <Table className="table-instituicao">
                    <thead>
                        <tr>
                            <th>Identificação</th>
                            <th>Nome</th>
                            <th>Endereco</th>
                            <th>Bairro</th>
                            <th>Localização</th>
                            <th>Cep</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            instituicoes.map((instituicao, index) => {
                                return (
                                    <tr key={index}>
                                        <td>{instituicao.id}</td>
                                        <td>{instituicao.nome}</td>
                                        <td>
                                            {instituicao.logradouro} , {instituicao.numero}
                                        </td>
                                        <td>{instituicao.bairro}</td>
                                        <td>{instituicao.cidade} - {instituicao.uf}</td>
                                        <td>{instituicao.cep}</td>
                                        <td>
                                            <Button value={instituicao.id} onClick={e => BuscarInformacoes(e)} variant="warning">Alterar</Button>
                                            <Button value={instituicao.id} onClick={e => ExcluirProfessor(e)} variant="danger">Remover</Button>
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

export default InstituicaoCrud