import axios from 'axios';
import React, { useState } from 'react'
import DangerAlertWidget from '../../../components/Alert/alert-danger';
import SuccesAlertWidget from '../../../components/Alert/alert-success';

import '../../../css/formStyle.css';

const initialValue = {
    billId: 0,
    houseId: 0,
    block: "",
    floor: 0,
    aptNo: 0,
    monthly: new Date(),
    price: 0,
    billDesc: ""
};

function AddBill() {
    const [formValue, setformValue] = useState(initialValue);

    //if entered in the input
    const handleChange = (event) => {
        setformValue({
            ...formValue,
            [event.target.name]: event.target.value

        });
    };

    //find house id by given house information from db
    const findHouseId = (block, floor, aptno) => {
        let houseId;
        axios.get('http://localhost:5036/api/House/find', { block, floor, aptno })
            .then((response) => {
                houseId = response.data.houseId;
                //setformValue({houseId: response.data.houseId});
                console.log(response.data.houseId);
            });
        return houseId;
    }

    //if the form is submitted
    const handleSubmit = (event) => {
        event.preventDefault();

        const resdata = {
            "billId": 0,
            "houseId": findHouseId(formValue.block,
                formValue.floor,
                formValue.aptNo),
            "monthly": formValue.monthly,
            "price": formValue.price,
            "billDesc": formValue.billDesc,
        };
        axios.post('http://localhost:5036/api/Bill', resdata)
            .then(response => {
                setformValue({ billId: response.data.billId });
                <SuccesAlertWidget />
                console.log(response.data);
            })
            .catch((error) => {
                <DangerAlertWidget />
                console.log(error);
            })
    }

    return (
        <>
            <div className='form-container'>
                <h1 className='formTitle'>Fatura Ekle</h1>
                <form className='addFormNear' onSubmit={handleSubmit}>
                    <div className='formItem'>
                        <select className='formSelect' name="month" id="month" value={formValue.monthly} onChange={handleChange}>
                            [
                            'Ocak',
                            'Şubat',
                            'Mart',
                            'Nisan',
                            'Mayıs',
                            'Haziran',
                            'Temmuz',
                            'Ağustos',
                            'Eylül',
                            'Ekim',
                            'Kasım',
                            'Aralık'
                        ].map((month, id) => (
                            <option value={month}>{month}</option>
                            )
                            );
                        </select>
                    </div>
                    <div className='formItem'>
                        <label>Blok</label>
                        <select className='formSelect' name="houseBlock" id="houseBlock" value={formValue.block} onChange={handleChange}>
                            <option value="A-block">A-block</option>
                            <option value="B-block">B-block</option>
                            <option value="C-block">C-block</option>
                            <option value="D-block">D-block</option>
                        </select>
                    </div>
                    <div className='formItem'>
                        <label>Kat</label>
                        <select className='formSelect' name="houseFloor" id="houseFloor" value={formValue.floor} onChange={handleChange}>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                        </select>
                    </div>
                    <div className='formItem'>
                        <label>Apatman Numarası</label>
                        <select className='formSelect' name="houseaptNo" id="houseaptNo" value={formValue.aptNo} onChange={handleChange}>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                        </select>
                    </div>
                    <div className='formItem'>
                        <label>Fatura Tutarı</label>
                        <input type="number" name='price' value={formValue.price} onChange={handleChange} />
                    </div>
                    <div className='formItem'>
                        <label>Fatura Türü</label>
                        <select className='formSelect' name="billDesc" id="billDesc" value={formValue.bilDesc} onChange={handleChange}>
                            <option value="Elektrik">Elektrik</option>
                            <option value="Su">Su</option>
                            <option value="Doğalgaz">Doğalgaz</option>
                        </select>
                    </div>
                    <br />
                    <button type='submit' className='formSubmitButton'>Ekle</button>
                </form>

            </div>
        </>
    )
}

export default AddBill