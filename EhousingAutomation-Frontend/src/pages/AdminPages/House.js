import React from 'react';
import { Button, Card, Col, Container, Row } from 'react-bootstrap';
import { BsHouseDoor } from 'react-icons/bs';
import { FcDataRecovery, FcDeleteDatabase, FcDataBackup, FcDatabase } from "react-icons/fc";

import '../../css/cardStyle.css';
import '../../css/buttonStyle.css';

function House() {
    return (
        <Container className='card-container'>
            <Row>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsHouseDoor className='sidebarTitleIcon' />Home
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Yeni Daire Ekleme</Card.Title>
                            <Button className='button'><FcDataRecovery/></Button>
                        </Card.Body>
                    </Card>
                </Col>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsHouseDoor className='sidebarTitleIcon' />Home
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Daireleri Listele</Card.Title>
                            <Button className='button'><FcDatabase/></Button>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsHouseDoor className='sidebarTitleIcon' />Home
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Daire Bilgileri Güncelleme</Card.Title>
                            <Button className='button'><FcDataBackup/></Button>
                        </Card.Body>
                    </Card>
                </Col>
                <Col className='card-col' xs={12} md>
                    <Card className='card'>
                        <Card.Header className='card-header'>
                            <BsHouseDoor className='sidebarTitleIcon' />Home
                        </Card.Header>
                        <Card.Body>
                            <Card.Title>Daire Silme</Card.Title>
                            <Button className='button'><FcDeleteDatabase/></Button>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}

export default House