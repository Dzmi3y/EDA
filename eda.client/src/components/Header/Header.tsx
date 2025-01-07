import {
  HeaderContainer,
  IconsGroupDiv,
  MotionButton,
  RootContainer,
} from "./styles";
import elephant from "../../assets/images/elephant.svg";
import cart from "../../assets/images/cart.svg";
import AccountDialog from "../AccountDialog/AccountDialog";

const Header = () => {
  const buttonAnimationSettings = {
    whileHover: { scale: 1.2 },
    whileTap: { scale: 0.95 },
  };

  return (
    <RootContainer>
      <HeaderContainer>
        <h1>Elephant</h1>
        <img src={elephant} alt="logo" />
        <h1>Shop</h1>
        <IconsGroupDiv>
          <AccountDialog />
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
