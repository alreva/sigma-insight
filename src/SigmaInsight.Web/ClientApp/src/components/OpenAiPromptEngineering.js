import React, {useEffect, useState} from 'react';
import {Button, FloatingLabel, Form, ListGroup, Stack} from "react-bootstrap";

export default () => {

  const statuses = {
    "idle": "idle",
    "loading": "loading",
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
    
    // call api
    var response = await fetch(`ai/prompt-engineering`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({prompt: question})
    })
    
    var data = await response.json();
    console.log(data);
    
    setAnswer(data.result);
    
  }
  
  return (
    <>
      <h1>OpenAI Prompt Engineering</h1>
      <p>This component demonstrates how the response from Open AI is altered with prompt engineering.</p>
      {status === statuses.loading && <p><em>Loading...</em></p>}

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
              <Form.Text className="text-muted">
                This is sent to Open AI endpoint and put as a prompt.
              </Form.Text>
            </div>
            <div>
              <Form.Control
                type={"text"}
                as={"textarea"}
                value={answer}
                readOnly
                placeholder={"Result will be displayed here."}/>
            </div>
            <div>
              <p>Pick from the predefined questions:</p>
              {
                [
                  "Who is Sigma Software CEO?",
                  "Who is Sigma Software Executive Vice President?",
                  "Who is Sigma Software Executive Vice President?" +
                  " Your response should contain his/her name and surname only," +
                  " several names if there are multiple"
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