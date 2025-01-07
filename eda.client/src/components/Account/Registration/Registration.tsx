import { ChangeEvent, FormEvent, useState } from "react";
import { Button, StyledForm, StyledInput } from "../styles";

export const Registration = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleUsernameChange = (event: ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };
  const handleEmailChange = (event: ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };
  const handlePasswordChange = (event: ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const buttonSettings = {
    whileHover: { scale: 1.02 },
    whileTap: { scale: 0.95 },
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    console.log("Username:", username);
    console.log("Email:", email);
    console.log("Password:", password);
  };
  return (
    <StyledForm onSubmit={handleSubmit}>
      <label htmlFor="username">Username</label>
      <StyledInput
        id="username"
        name="username"
        type="text"
        placeholder="username"
        value={username}
        onChange={handleUsernameChange}
      />
      <label htmlFor="email">Email</label>
      <StyledInput
        id="email"
        name="email"
        type="text"
        placeholder="email"
        value={email}
        onChange={handleEmailChange}
      />
      <label htmlFor="password">Password</label>
      <StyledInput
        id="password"
        name="password"
        type="password"
        placeholder="password"
        value={password}
        onChange={handlePasswordChange}
      />
      <Button {...buttonSettings} type="submit">
        Sign Up
      </Button>
    </StyledForm>
  );
};
