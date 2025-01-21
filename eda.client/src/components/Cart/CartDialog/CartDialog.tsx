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
import cartImage from "../../../assets/images/cart.svg";
import { Cart } from "../Cart";
import { useAppContext } from "../../../AppProvider";

const CartDialog = () => {
  const dialogRef = useRef<HTMLDialogElement>(null);
  const dialogContentRef = useRef<HTMLDivElement>(null);
  const appContext = useAppContext();
  //test
  useEffect(() => {
    appContext.updateCart([
      {
        count: 2,
        product: {
          id: "1",
          title: "Product 1",
          description: "",
          count: 1,
          imageUrl: "",
          price: 1,
        },
      },
      {
        count: 3,
        product: {
          id: "2",
          title: "Product 2",
          description: "",
          count: 1,
          imageUrl: "",
          price: 1,
        },
      },
      {
        count: 4,
        product: {
          id: "3",
          title: "Product 3",
          description: "",
          count: 1,
          imageUrl: "",
          price: 1,
        },
      },
    ]);
  }, []);

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
