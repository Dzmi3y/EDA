import { useEffect, useRef } from "react";
import {
  CloseButton,
  Container,
  Dialog,
  Header,
  IconButton,
  Title,
} from "./styles";
import cartImage from "../../../assets/images/cart.svg";
import { Cart } from "../Cart";

const CartDialog = () => {
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
        <img
          src={cartImage}
          alt="cart"
          width={50}
          style={{ marginTop: "8px" }}
        />
      </IconButton>

      <Dialog ref={dialogRef}>
        <Container ref={dialogContentRef}>
          <form method="dialog">
            <Header>
              <Title>Cart</Title>
              <CloseButton {...buttonAnimationSettings} type="submit">
                &#x2715;
              </CloseButton>
            </Header>
          </form>
          <div style={{ position: "relative" }}>
            <Cart />
          </div>
        </Container>
      </Dialog>
    </>
  );
};

export default CartDialog;
