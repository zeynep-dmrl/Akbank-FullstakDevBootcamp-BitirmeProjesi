import React from 'react'
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import { FcExport, FcComments } from "react-icons/fc";

import './header.css'


function Header(props) {
    return (
        <>
            <Navbar bg="light" expand="lg" className='top-bar'>
                <Container className='top-bar-container'>
                    <Navbar.Brand href="#home" className='brand'>E-HOUSİNG AUTOMATİON</Navbar.Brand>
                    <Navbar.Collapse id="basic-navbar-nav"  className="right">
                        <Nav className="me-auto">
                            <Nav.Link>
                                <FcComments className='icon'/>
                            </Nav.Link>
                            <Nav.Link>
                                <FcExport className='icon'/>
                            </Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    )
}

export default Header