import React, { useState } from 'react';
import axios from 'axios';
import './App.css';

function App() {
  const [messages, setMessages] = useState([]);
  const [input, setInput] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const apiBaseUrl = 'http://localhost:5000/api/chat';

  const sendMessage = async () => {
    if (!input.trim()) return;

    const newMessages = [...messages, { role: 'user', text: input }];
    setMessages(newMessages);
    setInput('');
    setIsLoading(true);

    try {
      const response = await axios.post(apiBaseUrl, { message: input });
      const botReply = response.data.response;
      setMessages([...newMessages, { role: 'bot', text: botReply }]);
    } catch (error) {
      setMessages([...newMessages, { role: 'bot', text: 'Error connecting to server.' }]);
    }

    setIsLoading(false);
  };

  const onInputKeyPress = (e) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault();
      sendMessage();
    }
  };

  return (
    <div className="App">
      <h1>Azure OpenAI Chatbot</h1>
      <div className="chat-window">
        {messages.map((msg, index) => (
          <div
            key={index}
            className={`message ${msg.role === 'user' ? 'user-message' : 'bot-message'}`}
          >
            <span>{msg.text}</span>
          </div>
        ))}
        {isLoading && <div className="message bot-message">Typing...</div>}
      </div>
      <textarea
        rows={3}
        placeholder="Write your message..."
        value={input}
        onChange={(e) => setInput(e.target.value)}
        onKeyPress={onInputKeyPress}
      />
      <button onClick={sendMessage} disabled={isLoading || !input.trim()}>Send</button>
    </div>
  );
}

export default App;
