import React from 'react';
import { Button, Card, Col, Container, Row } from 'react-bootstrap';
import { FcDataRecovery, FcDeleteDatabase, FcDataBackup, FcDatabase } from "react-icons/fc";
import { BsPerson } from 'react-icons/bs';

import '../../css/cardStyle.css';
import '../../css/buttonStyle.css';

function Dues() {
    return (
        <Container className='card-container'>
            <Row>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsPerson className='sidebarTitleIcon' />Dues
                        </Card.Header>
                        <Card.Body>
                            <Card.Title >Yeni Aidat Ekleme</Card.Title>
                            <Button className='button'><FcDataRecovery/></Button>
                        </Card.Body>
                    </Card>
                </Col>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsPerson className='sidebarTitleIcon' />Dues
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Aidatları Listele</Card.Title>
                            <Button className='button'><FcDatabase/></Button>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsPerson className='sidebarTitleIcon' />Dues
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Aidat Bilgileri Güncelleme</Card.Title>
                            <Button className='button'><FcDataBackup/></Button>
                        </Card.Body>
                    </Card>
                </Col>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsPerson className='sidebarTitleIcon' />Dues
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Aidat Silme</Card.Title>
                            <Button className='button'><FcDeleteDatabase/></Button>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}

export default Dues