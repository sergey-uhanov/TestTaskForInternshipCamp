import React, { useState } from 'react';

const App = () => {
    const [email, setEmail] = useState('');
    const [file, setFile] = useState(null);
    const [errorMessage, setErrorMessage] = useState('');


    const handleEmailChange = (event) => {
        setEmail(event.target.value);
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file && file.name.endsWith('.docx')) {
            setFile(file);
        } else {
            setErrorMessage('Please upload a .docx file');
            setFile(null);
        }
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        if (!email.includes('@')) {
            setErrorMessage('Please enter a valid e-mail address');
        } else if (!file) {
            setErrorMessage('Please download the .docx file');
        } else {
            setErrorMessage('The form has been successfully submitted!');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Email:
                <input type="email" value={email} onChange={handleEmailChange} />
            </label>
            <label>
                Download the file:
                <input type="file" onChange={handleFileChange} />
            </label>
            <button type="submit">Send</button>
            {errorMessage}
        </form>
    );
};

export default App;
