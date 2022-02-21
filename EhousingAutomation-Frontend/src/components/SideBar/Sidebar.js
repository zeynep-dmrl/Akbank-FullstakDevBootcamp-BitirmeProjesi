import React from 'react';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { BsFileEarmarkText, BsHouseDoor, BsReceipt, BsPerson} from 'react-icons/bs';


import './sidebar.css';

function Sidebar(props) {
    return (
        <>
            <div className="sidebar">
                <div className="sideWrapper">
                    <div className="sidebarMenu">
                        
                        {/* <Link to={"../../pages/AdminPages/House"} params={{}} className="link" > */}
                        <h3 className="sidebarTitle sidebarList sidebarListItem"><BsHouseDoor className='sidebarTitleIcon' />Home</h3>
                        {/* </Link> */}

                        {/* <Link to={"../../pages/AdminPages/Bill"} params={{}} className="link" > */}
                        <h3 className="sidebarTitle sidebarList sidebarListItem"><BsFileEarmarkText className='sidebarTitleIcon' />Bills</h3>
                        {/* </Link> */}

                        {/* <Link to={"../../pages/AdminPages/Dues"} params={{}} className="link" > */}
                        <h3 className="sidebarTitle sidebarList sidebarListItem"><BsReceipt className='sidebarTitleIcon' /> Dues</h3>
                        {/* </Link> */}

                        {/* <Link to={"../../pages/AdminPages/User"} params={{}} className="link" > */}
                        <h3 className="sidebarTitle sidebarList sidebarListItem"><BsPerson className='sidebarTitleIcon' /> Users</h3>
                        {/* </Link> */}

                    </div>
                </div >
            </div >
        </>
    )
}

export default Sidebar