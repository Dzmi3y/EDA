import { ChangeEvent, FormEvent, useState } from "react";
import { Button, ErrorMessage, StyledForm, StyledInput } from "../styles";
import { AuthorizationRequestData } from "../../../Data/requests/AuthorizationRequestData";
import { authorization } from "../../../services/ApiService";
import LoadingPanda from "../../LoadingPanda/LoadingPanda";
import { useAuth } from "../../../AuthProvider";

export const Authorization: React.FC<{ closeDialogHandler: () => void }> = ({
  closeDialogHandler,
}) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [isLoaderVisible, setIsLoaderVisible] = useState<boolean>(false);

  const authData = useAuth();

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
    setIsLoaderVisible(true);
    setErrorMessage("");
    let requestData: AuthorizationRequestData = {
      email: email,
      password: password,
    };

    try {
      const response = await authorization(requestData);

      const { accessToken, expiresIn, refreshToken } = response.payload;
      console.log(accessToken);
      console.log(expiresIn);
      console.log(refreshToken);

      if (!response.errorMessage) {
        authData.updateAuthData(response.payload);
      }
      setErrorMessage(response.errorMessage);
    } catch (e) {
      setErrorMessage("Server error");
    }

    setIsLoaderVisible(false);
    closeDialogHandler();
  };
  return (
    <StyledForm onSubmit={handleSubmit}>
      <div style={{ zIndex: 1 }}>
        <LoadingPanda isVisible={isLoaderVisible} />
      </div>
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
