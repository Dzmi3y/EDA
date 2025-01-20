import { ChangeEvent, FormEvent, useState } from "react";
import { Button, ErrorMessage, StyledForm, StyledInput } from "../styles";
import { registration } from "../../../services/ApiService";
import { RegistrationRequestData } from "../../../Data/requests/RegistrationRequestData";
import LoadingPanda from "../../LoadingPanda/LoadingPanda";

export const Registration: React.FC<{ closeDialogHandler: () => void }> = ({
  closeDialogHandler,
}) => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [isLoaderVisible, setIsLoaderVisible] = useState<boolean>(false);

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

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setIsLoaderVisible(true);
    setErrorMessage("");
    let requestData: RegistrationRequestData = {
      email: email,
      name: username,
      password: password,
    };

    try {
      const response = await registration(requestData);
      console.log(response);
      setErrorMessage(response.errorMessage);
    } catch (e) {
      setErrorMessage("Server error");
      console.log(e);
    }
    setIsLoaderVisible(false);
    closeDialogHandler();
  };

  return (
    <StyledForm onSubmit={handleSubmit}>
      <div style={{ zIndex: 1 }}>
        <LoadingPanda isVisible={isLoaderVisible} />
      </div>
      <label htmlFor="username">Username</label>
      <StyledInput
        id="username"
        name="username"
        type="text"
        placeholder="username"
        value={username}
        onChange={handleUsernameChange}
        required
      />
      <label htmlFor="email">Email</label>
      <StyledInput
        id="email"
        name="email"
        type="email"
        placeholder="email"
        value={email}
        onChange={handleEmailChange}
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
        pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
        title="Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters"
        required
      />
      {!!errorMessage && <ErrorMessage>{errorMessage}</ErrorMessage>}
      <Button {...buttonSettings} type="submit">
        Sign Up
      </Button>
    </StyledForm>
  );
};
