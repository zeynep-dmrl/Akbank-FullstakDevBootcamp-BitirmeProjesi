import React, { useState } from 'react';
import { FcCheckmark } from "react-icons/fc";
import Alert from 'react-bootstrap/Alert'

function SuccesAlertWidget() {
    const [show, setShow] = useState(true);
    return (
        <>
        <Alert variant = "success" onClose={() => setShow(false) } dismissible>
            <p><FcCheckmark/> İşlem Başarılı</p>
        </Alert>
        </>
    )
}

export default SuccesAlertWidget