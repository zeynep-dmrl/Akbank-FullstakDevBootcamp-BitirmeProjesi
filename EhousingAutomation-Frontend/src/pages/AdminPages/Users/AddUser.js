import axios from 'axios';
import React, { useState } from 'react';
import SuccesAlertWidget from '../../../components/Alert/alert-success';
import DangerAlertWidget from '../../../components/Alert/alert-danger';

import '../../../css/formStyle.css';
const initialValue = {
    userId:0,
    firstName: "",
    lastName: "",
    tcNo: "",
    email: "",
    phoneNumber: "",
    carInfo: "",
    password:""
};

function AddUser() {
    const [formValue, setformValue] = useState(initialValue);
    
    //random password generator
    function generatePassword() {
        var length = 8,
            charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
            retVal = "";
        for (var i = 0, n = charset.length; i < length; ++i) {
            retVal += charset.charAt(Math.floor(Math.random() * n));
        }
        return retVal;
    }

    //if entered in the input
    const handleOnchange = (e) => {
        setformValue({...formValue, [e.target.name] : e.target.value,password:generatePassword()});
    };

    //if the form is submitted
    const handleSubmit = (event) => {
        event.preventDefault();
        // store the states in the form data
        
        const resdata = {
            "userId": 0,
            "firstName": formValue.firstName,
            "lastName" : formValue.lastName,
            "tcNo": formValue.tcNo ,
            "email": formValue.email,
            "phoneNumber": formValue.phoneNumber,
            "carInfo": formValue.carInfo,
            "password": formValue.password
        };
        axios.post('http://localhost:5036/api/User', resdata)
        .then(response => {
            setformValue({userId : response.data.userId});
            <SuccesAlertWidget/>
            console.log(response);
            console.log(response.data);
        })
        .catch((error) => {
            <DangerAlertWidget/>
            console.log(error);
        });
    };

  return (
    <>
    <div className='form-container'>
        <h1 className='formTitle'>Yeni Kullanıcı</h1>
        <form className='addForm' onSubmit={handleSubmit}>
            <div className='formItem'>
            <label>Kullanıcı Adı</label>
            <input type = "text" name='firstName' value={formValue.firstName} onChange={handleOnchange}/>
            </div>
            <div className='formItem'>
            <label>Kullanıcı Soyadı</label>
            <input type = "text" name='lastName' value={formValue.lastName} onChange={handleOnchange}/>
            </div>
            <div className='formItem'>
            <label>Kullanıcı Kimlik Numarası</label>
            <input type = "text" name='tcNo' value={formValue.tcNo} onChange={handleOnchange}/>
            </div>
            <div className='formItem'>
            <label>Kullanıcı Mail Adresi</label>
            <input type = "email" name='email' value={formValue.email} onChange={handleOnchange}/>
            </div>    
            <div className='formItem'>
            <label>Kullanıcı Telefon Numarası</label>
            <input type = "text" name='phoneNumber' value={formValue.phoneNumber} onChange={handleOnchange}/>
            </div>
            <div className='formItem'>
            <label>Kullanıcı Araba Bilgisi</label>
            <input type = "text" name='carInfo' value={formValue.carInfo} onChange={handleOnchange}/>
            </div>
            <br/>
            <button type='submit' className='formSubmitButton'>Ekle</button>
        </form>
    </div>
    </>
  )
}

export default AddUser