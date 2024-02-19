import React, { useState } from 'react';

const App = () => {
    const [email, setEmail] = useState('');
    const [file, setFile] = useState(null);
    const [errorMessage, setErrorMessage] = useState('');
    const [Message, setMessage] = useState('');

    const handleEmailChange = (event) => {
        setEmail(event.target.value);
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file && file.name.endsWith('.docx')) {
            setFile(file);
            setErrorMessage('');
        } else {
            setFile(null);
            setErrorMessage('Please upload a .docx file');
        }
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (!email.includes('@')) {
            setErrorMessage('Please enter a valid e-mail address');
        } else if (!file) {
            setErrorMessage('Please upload the .docx file');
        } else {            
            const formData = new FormData();
            formData.append('file', file);
            formData.append('email', email);

            try {                
                const response = await fetch('https://testtaskforinternshipcamp.azurewebsites.net/Files', {
                    method: 'POST',
                    body: formData,
                });
                console.log(response)
                if (response.status == 200) {
                    setMessage('The file has been successfully uploaded!');
                    setEmail('');
                    setFile(null); 

                } else {
                    setErrorMessage('An error occurred while uploading the file.');
                }
            } catch (error) {
                console.error('Error uploading file:', error);
                setErrorMessage('An error occurred while uploading the file.');
            }
        }
    };  


    return (

        <div className="ring">
            <i style={{ "--clr": "#00ff0a" }}></i>
            <i style={{ "--clr": "#ff0057" }}></i>
            <i style={{ "--clr": "#fffd44" }}></i>
            <form className="form-input" onSubmit={handleSubmit}>
                <h2>Download  file</h2>
                <div className="inputBx">
                    <input type="email" value={email} onChange={handleEmailChange} />
                </div>
                <div className="inputBx">
                    <input type="file" accept=".docx" onChange={handleFileChange} />
                </div>
                {errorMessage && <div className="errorMessage">{errorMessage}</div>}
                <div className="inputBx">
                    <button type="submit">Send</button>                 
                    {Message && <div className="message">{Message}</div>}
                </div>
            </form>
        </div>

    );
};

export default App;
