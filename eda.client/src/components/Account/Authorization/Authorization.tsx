import { ChangeEvent, FormEvent, useState } from "react";
import { Button, ErrorMessage, StyledForm, StyledInput } from "../styles";
import { AuthorizationRequestData } from "../../../Data/AuthorizationRequestData";
import { authorization } from "../../../services/ApiService";

export const Authorization = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleLoginChange = (event: ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };
  const handlePasswordChange = (event: ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const buttonSettings = {
    whileHover: { scale: 1.02 },
    whileTap: { scale: 0.95 },
  };

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    let requestData: AuthorizationRequestData = {
      email: email,
      password: password,
    };

    const response = await authorization(requestData);
    console.log(response.payload.accessToken);
    console.log(response.payload.expiresIn);
    console.log(response.payload.refreshToken);
    setErrorMessage(response.errorMessage);
  };
  return (
    <StyledForm onSubmit={handleSubmit}>
      <label htmlFor="login">Email</label>
      <StyledInput
        id="email"
        name="email"
        type="email"
        placeholder="email"
        value={email}
        onChange={handleLoginChange}
        required
      />
      <label htmlFor="password">Password</label>
      <StyledInput
        id="password"
        name="password"
        type="password"
        placeholder="password"
        value={password}
        onChange={handlePasswordChange}
        required
      />
      {!!errorMessage && <ErrorMessage>{errorMessage}</ErrorMessage>}
      <Button {...buttonSettings} type="submit">
        Sign In
      </Button>
    </StyledForm>
  );
};
