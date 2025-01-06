import { useEffect, useRef } from "react";
import { CloseButton, Container, Dialog, Header, IconButton } from "./styles";
import account from "../../assets/images/account.svg";

const AccountDialog = () => {
  const dialogRef = useRef<HTMLDialogElement>(null);
  const dialogContentRef = useRef<HTMLDivElement>(null);
  const handleClickOutside = (event: MouseEvent) => {
    if (
      dialogRef.current &&
      !dialogContentRef.current?.contains(event.target as Node)
    ) {
      dialogRef.current?.close();
    }
  };

  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
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
        </Container>
      </Dialog>
    </>
  );
};

export default AccountDialog;
