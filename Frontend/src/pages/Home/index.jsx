import React from 'react'
import { Jumbotron, Button, Container, Row, Col, Card } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBookOpen, faLaughSquint, faChalkboardTeacher } from '@fortawesome/free-solid-svg-icons'
import './style.css';
import bookHome from '../../assets/vetores/book-home.svg'
import proffHome from '../../assets/vetores/proff-home.svg'
import Menu from '../../components/menu';
import Footer from '../../components/footer';

const Home = () => {
    return (
        <>
            <Menu />
            <section className="jumbo">
                <Jumbotron className="jumbo-fundo">
                    <div className="jumbo-content">
                        <h1>Uma nova forma de estudar!</h1>
                        <hr />
                        <p>
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Adipisci fugiat quod molestias omnis quam,
                            provident ipsum eum. Illo non consequuntur nihil cum. Voluptatibus aliquam fuga totam, error qui
                            consequuntur suscipit?
                        </p>
                            <Button className="button-confira" href="/cadastro">Confira mais ...</Button>
                    </div>
                </Jumbotron>
            </section>
            <section className="cards-content">
                <h2>Nosso diferencial!</h2>
                <Container>
                    <Row>
                        <Col>
                            <Card className="card-items" style={{ width: '18rem' }}>
                                <FontAwesomeIcon icon={faBookOpen} size="6x" color="#00C2EE"/>
                                <Card.Body>
                                    <Card.Text className="text-cards">
                                        Some quick example text to build on the card title and make up the bulk of
                                        the card's content.
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Col>
                            <Card className="card-items" style={{ width: '18rem' }}>
                            <FontAwesomeIcon icon={faLaughSquint} size="6x" color="#00D65F"/>
                                <Card.Body>
                                    <Card.Text className="text-cards">
                                        Some quick example text to build on the card title and make up the bulk of
                                        the card's content.
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Col>
                            <Card className="card-items" style={{ width: '18rem' }}>
                            <FontAwesomeIcon icon={faChalkboardTeacher} size="6x" color="#FF271C"/>
                                <Card.Body>
                                    <Card.Text className="text-cards">
                                        Some quick example text to build on the card title and make up the bulk of
                                        the card's content.
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                    </Row>
                </Container>
                <hr />
            </section>
            <section className="info-content">
                <Container>
                    <Row className="row-normal">
                        <div className="imagem-style">
                            <img src={proffHome} alt="Illustração de um professor"/>
                        </div>
                        <div className="text-info">
                            <h3>Ajudando nossos Professores</h3>
                            <hr />
                            <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit.
                            Similique repudiandae nesciunt fugiat ullam asperiores officia
                            amet,sequi sit temporibus corporis ipsum delectus, minus est
                            dolore nihil esse, tempora corrupti voluptatibus.</p>
                        </div>
                    </Row>
                    <Row className="row-reverse">
                        <div className="imagem-style">
                            <img src={bookHome} alt="Illustração de um aluno"/>
                        </div>
                        <div className="text-info text-info-reverse">
                            <h3>Ajudando nossos Alunos</h3>
                            <hr />
                            <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit.
                            Similique repudiandae nesciunt fugiat ullam asperiores officia amet, sequi sit temporibus corporis ipsum delectus, minus est dolore nihil esse, tempora corrupti voluptatibus.</p>
                        </div>
                    </Row>
                </Container>
            </section>
            <Footer />
        </>
    )   
}

export default Home;