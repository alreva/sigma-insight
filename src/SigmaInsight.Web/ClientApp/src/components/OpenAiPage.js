import React, {useEffect, useState} from 'react';
import {Button, FloatingLabel, Form, ListGroup, Spinner, Stack} from "react-bootstrap";

export default ({ controller }) => {

  const statuses = {
    "idle": "idle",
    "loading": "loading",
    "success": "success",
    "error": "error"
  };
  const [status, setStatus] = useState(statuses.idle);
  const [question, setQuestion] = useState('');
  const [answer, setAnswer] = useState('');

  const handleQuestionChange = (evt) => {
    evt.preventDefault();
    setQuestion(evt.target.value);
  }
  
  const handleSelect = (evt) => {
    setQuestion(evt.target.value);
  }
  
  const handleSubmit = async (evt) => {
    evt.preventDefault();
    console.log(question);
    
    setStatus(statuses.loading);
    
    // call api
    const response = await fetch(`api/ai/${controller.httpEndpoint}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({prompt: question})
    })
    
    const data = await response.json();
    console.log(data);
    
    setAnswer(data.result);
    setStatus(statuses.success);
  }
  
  const getValue = () => {
    if (status === statuses.success) {
      return answer;
    }
    
    if (status === statuses.error) {
      return "Error occurred. Please try again.";
    }
    
    if (status === statuses.loading) {
      return <Spinner />
    }
    
    return "Response will be shown here";
  }
  
  return (
    <>
      <h1>{controller.pageTitle}</h1>
      <p>{controller.pageDescription}</p>

      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="formQuestion">
          <Stack gap={4}>
            <div>
              <FloatingLabel
                controlId="floatingInput"
                label="Question"
                className="mb-3"
              >
                <Form.Control
                  type="text"
                  placeholder="Enter your question"
                  value={question}
                  onChange={handleQuestionChange}
                />
              </FloatingLabel>
              <Form.Text>
                This is sent to Open AI endpoint and put as a prompt.
              </Form.Text>
            </div>
            
            <div>
              <div style={{ minHeight: 120 }}>
                {getValue()}
              </div>
            </div>
            <div>
              <p>Pick from the predefined questions:</p>
              {
                [
                  "Who is Sigma Software CEO?",
                  "Who is Sigma Software Executive Vice President?",
                  "Who is Sigma Software Vice President?",
                  "Who is Sigma Software Chief Innovation Officer?"
                ]
                  .map((q, i) =>
                    <p key={i}><Button value={q} variant={"light"} onClick={handleSelect}> {q} </Button></p>
                  )
              }
            </div>
            <div>
              <Button variant="primary" type="submit" disabled={question === ""}>
                Send
              </Button>
            </div>
          </Stack>
        </Form.Group>
      </Form>
    </>
  );
}