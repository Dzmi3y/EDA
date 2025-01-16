import { useEffect, useRef, useState } from "react";
import {
  CloseButton,
  Container,
  Dialog,
  Header,
  IconButton,
  SwitchButton,
  Title,
} from "./styles";
import account from "../../../assets/images/account.svg";
import { Authorization } from "../Authorization/Authorization";
import { Registration } from "../Registration/Registration";

const AccountDialog = () => {
  const [isAuthorization, setIsAuthorization] = useState<boolean>(true);

  const dialogRef = useRef<HTMLDialogElement>(null);
  const dialogContentRef = useRef<HTMLDivElement>(null);

  const handleSwitchDialogButton = () => {
    setIsAuthorization(!isAuthorization);
  };

  const closeDialogHandler: () => void = () => {
    dialogRef.current?.close();
  };

  const handleClickOutside = (event: MouseEvent) => {
    if (
      dialogRef.current &&
      !dialogContentRef.current?.contains(event.target as Node)
    ) {
      dialogRef.current?.close();
    }
  };

  const buttonAnimationSettings = {
    whileHover: { scale: 1.2 },
    whileTap: { scale: 0.95 },
  };

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);
  return (
    <>
      <IconButton
        {...buttonAnimationSettings}
        onClick={() => dialogRef.current?.showModal()}
      >
        <img src={account} alt="account" width={50} />
      </IconButton>

      <Dialog ref={dialogRef}>
        <Container ref={dialogContentRef}>
          <form method="dialog">
            <Header>
              <CloseButton {...buttonAnimationSettings} type="submit">
                &#x2715;
              </CloseButton>
            </Header>
          </form>
          <div style={{ position: "relative" }}>
            {isAuthorization && (
              <>
                <Title>Authorization</Title>
                <SwitchButton onClick={handleSwitchDialogButton}>
                  to registration
                </SwitchButton>
                <Authorization closeDialogHandler={closeDialogHandler} />
              </>
            )}

            {!isAuthorization && (
              <>
                <Title>Registration</Title>
                <SwitchButton onClick={handleSwitchDialogButton}>
                  to authorization
                </SwitchButton>
                <Registration closeDialogHandler={closeDialogHandler} />
              </>
            )}
          </div>
        </Container>
      </Dialog>
    </>
  );
};

export default AccountDialog;
