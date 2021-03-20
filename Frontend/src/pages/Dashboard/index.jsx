import React, { Component } from 'react'
import './style.css'
import Menu from '../../components/menu';
import Footer from '../../components/footer';
import { Button } from 'react-bootstrap'
import ProfessoresCrud from './Professores/index'
import CursoCrud from './Cursos/index'
import TurmaCrud from './Turmas/index'
import InstituicaoCrud from './Instituicoes/index'
import imgFundo from '../../assets/vetores/question-dash.svg'

class Dashboard extends Component{
    constructor(){
        super();
        this.state = {render:''}
    }

    selectRender(){

        switch(this.state.render)
        {
            case 'Professores' : return <ProfessoresCrud />
            case 'Cursos' : return <CursoCrud />
            case 'Turmas' : return <TurmaCrud />
            case 'Instituicoes' : return <InstituicaoCrud />
        }
    }

    render(){
        return (
            <>
                <Menu />
                <section className="dash-content">
                    <section className="maps-list">
                        <h2>Gerenciar os dados do sistemas</h2>
                        <hr />
                        <div className="list-item">
                            <h4>Professores</h4>
                            <Button variant="info" onClick={e => this.setState({ render : 'Professores'})}>Gerenciar</Button>
                        </div>
                        <div className="list-item">
                            <h4>Cursos</h4>
                            <Button variant="info" onClick={e => this.setState({ render : 'Cursos'})}>Gerenciar</Button>
                        </div>
                        <div className="list-item">
                            <h4>Turmas</h4>
                            <Button variant="info" onClick={e => this.setState({ render : 'Turmas'})}>Gerenciar</Button>
                        </div>
                        <div className="list-item">
                            <h4>Instituicoes</h4>
                            <Button variant="info" onClick={e => this.setState({ render : 'Instituicoes'})}>Gerenciar</Button>
                        </div>
                    </section>
                    <section className="admin-content">
                    {
                        // this.selectRender()
                        this.state.render === '' ? 
                            <div className="content-dash">
                                <img src={imgFundo} alt="Imagem de um ponto de interrogação"/>
                                <h2>Selecione uma categoria para administrar suas informações</h2>
                            </div>
                            : this.selectRender()
                    }
                    </section>
                </section>
                <Footer />
            </>
        )
    }
}

export default Dashboard