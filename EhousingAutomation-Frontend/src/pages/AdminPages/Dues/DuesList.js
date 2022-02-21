import { DataGrid } from '@mui/x-data-grid';
import axios from 'axios';
import React, { useEffect, useState } from 'react';

import '../../../css/tableListStyle.css';


function DuesList() {

    const [data, setData] = useState();

    useEffect(() => {
        axios.get('http://localhost:5036/api/Dues')
        .then((response) => {
            console.log(response.data);
            setData(response.data);
        })
    }, []);

    const columns = [
        {field: 'user', headerName: 'Kİracı/Sahibi', width:120},
        {field: 'price', headerName: 'Tutar', width:75},
        {field: 'monthly', headerNmae: 'Ay', width:90},
        {field: 'duesDesc', headerName:'Türü', width:120},
        {field: 'isPaid', headerName: 'Durum/Sahibi', width:105}
    ];

    return (
        <>
            <div className="container">
                
                <div className="DuesList">
                    <h3 className="title">Aidatlar</h3>
                    <div className="table" style={{ height: 570, width: '100%' }}>
                        <DataGrid
                            getRowId={(data) => data.id}
                            rows={data}
                            columns={columns}
                        /> 
                    </div>
                </div>

            </div>
        </>
    )
}

export default DuesList