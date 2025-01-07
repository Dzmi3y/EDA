import { ChangeEvent, FormEvent, useState } from "react";
import { Button, StyledForm, StyledInput, Title } from "./styles";

export const Authorization = () => {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");

  const handleLoginChange = (event: ChangeEvent<HTMLInputElement>) => {
    setLogin(event.target.value);
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
    console.log("Login:", login);
    console.log("Password:", password);
  };
  return (
    <StyledForm onSubmit={handleSubmit}>
      <Title>Authorization</Title>
      <label htmlFor="login">Login</label>
      <StyledInput
        id="login"
        name="login"
        type="text"
        placeholder="login"
        value={login}
        onChange={handleLoginChange}
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
        Login
      </Button>
    </StyledForm>
  );
};
