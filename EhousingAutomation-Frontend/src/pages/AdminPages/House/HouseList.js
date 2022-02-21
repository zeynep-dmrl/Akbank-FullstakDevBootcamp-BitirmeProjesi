import { DataGrid } from '@mui/x-data-grid';
import axios from 'axios';
import React, { useEffect, useState } from 'react';

import '../../../css/tableListStyle.css';


function HouseList() {

    const [data, setData] = useState();

    useEffect(() => {
        axios.get('http://localhost:5036/api/House')
        .then((response) => {
            console.log(response.data);
            setData(response.data);
        })
    }, []);


    const dataHandle = () =>{
        setData(data.map((item) => {
            if(item.isFull == 'yes')
            {
                item.isFull = 'Dolu';
            }else{
                item.isFull = 'Boş';
            }
        })
        );
        console.log(data);
    }
    const columns = [
        {field: 'block', headerName: 'Block', width:100},
        {field: 'floor', headerName: 'Kat', width:75},
        {field: 'aptNo', headerNmae: 'AptNo', width:75},
        {field: 'isFull', headerName:'Durum', width:105},
        {field: 'ownerOrtenant', headerName: 'Kiracı/Sahibi', width:120}
    ];

    return (
        <>
            <div className="container">
                
                <div className="HouseList">
                    <h3 className="title"> Sitedeki Mevcut Evler</h3>
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

export default HouseList