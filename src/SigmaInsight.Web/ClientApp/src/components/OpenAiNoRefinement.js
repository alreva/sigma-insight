import React, {useEffect, useState} from 'react';
import {Button, Form, ListGroup, Stack, FloatingLabel} from "react-bootstrap";

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
    var response = await fetch(`api/ai/no-refinement`, {
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
      <h1>OpenAI No Refinement</h1>
      <p>This component demonstrates the responses from unmodified OpenAI model with no previous context.</p>
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
              <ListGroup>
                {
                  [
                    "Who is Sigma Software CEO?",
                    "Who is Sigma Executive Vice President?"]
                    .map((q, i) =>
                      <ListGroup.Item key={i} variant={"light"}>
                        <Button value={q} variant={"light"} onClick={handleSelect}> {q} </Button>
                      </ListGroup.Item>
                    )
                }
              </ListGroup>
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