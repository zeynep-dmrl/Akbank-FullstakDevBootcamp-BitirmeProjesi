import React, { useState } from 'react';
import { AiOutlineClose } from "react-icons/ai";
import Alert from 'react-bootstrap/Alert'

function DangerAlertWidget() {
    const [show, setShow] = useState(true);
    return (
        <>
        <Alert variant = "danger" onClose={() => setShow(false) } dismissible>
            <p><AiOutlineClose/> İşlem Başarısız</p>
        </Alert>
        </>
    )
}

export default DangerAlertWidget