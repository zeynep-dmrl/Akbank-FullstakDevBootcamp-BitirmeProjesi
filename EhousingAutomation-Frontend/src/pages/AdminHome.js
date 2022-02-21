import React from 'react';

import Header from '../components/Header/Header';
import Sidebar from '../components/SideBar/Sidebar';

import './adminHome.css'
import Bill from './AdminPages/Bill';
import Dues from './AdminPages/Dues';
import House from './AdminPages/House';
import HouseList from './AdminPages/House/HouseList';
import User from './AdminPages/User';
import AddUser from './AdminPages/Users/AddUser';

function AdminHome() {
    return (
        <>
            <Header name = "Zeynep" lastName="Demirel" />
            <div className="admin-container">
                <Sidebar />
                <div className="home">
                    <AddUser/>
                </div>
            </div>

        </>
  )
}

export default AdminHome