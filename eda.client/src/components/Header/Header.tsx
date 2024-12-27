import React from "react";
import {
  HeaderContainer,
  IconsGroupDiv,
  MotionButton,
  RootContainer,
} from "./styles";
import elephant from "../../assets/images/elephant.svg";
import cart from "../../assets/images/cart.svg";
import account from "../../assets/images/account.svg";

const Header = () => {
  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };
  return (
    <RootContainer>
      <HeaderContainer>
        <h1>Elephant</h1>
        <img src={elephant} alt="logo" />
        <h1>Shop</h1>
        <IconsGroupDiv>
          <MotionButton {...buttonAnimationSettings}>
            <img src={account} alt="account" width={50} />
          </MotionButton>
          <MotionButton {...buttonAnimationSettings}>
            <img
              style={{ marginTop: "8px" }}
              src={cart}
              alt="cart"
              width={50}
            />
          </MotionButton>
        </IconsGroupDiv>
      </HeaderContainer>
    </RootContainer>
  );
};

export default Header;
