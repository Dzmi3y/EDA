import {
  HeaderContainer,
  IconsGroupDiv,
  MotionButton,
  RootContainer,
} from "./styles";
import elephant from "../../assets/images/elephant.svg";
import logout from "../../assets/images/logout.svg";
import AccountDialog from "../Account/AccountDialog/AccountDialog";
import { useAppContext } from "../../AppProvider";
import { signout } from "../../services/ApiService";
import { AuthLocalStorageService } from "../../services/AuthLocalStorageService";
import CartDialog from "../Cart/CartDialog/CartDialog";

const Header = () => {
  const buttonAnimationSettings = {
    whileHover: { scale: 1.2 },
    whileTap: { scale: 0.95 },
  };

  const contextData = useAppContext();

  if (!contextData.accessToken) {
    if (AuthLocalStorageService.isAuthDataPresent()) {
      const auth = AuthLocalStorageService.getAuthData();
      contextData.updateAuthData(auth);
    }
  }

  const exitButtonClickHandler = async () => {
    contextData.updateAuthData(null);
    AuthLocalStorageService.removeAuthData();
    await signout({ refreshToken: contextData.refreshToken });
  };

  return (
    <RootContainer>
      <HeaderContainer>
        <h1>Elephant</h1>
        <img src={elephant} alt="logo" />
        <h1>Shop</h1>
        <IconsGroupDiv>
          {!contextData.accessToken && <AccountDialog />}
          {contextData.accessToken && (
            <MotionButton
              onClick={exitButtonClickHandler}
              {...buttonAnimationSettings}
            >
              <img
                style={{ marginTop: "8px" }}
                src={logout}
                alt="logout"
                height={41}
                width={50}
              />
            </MotionButton>
          )}
          <CartDialog />
        </IconsGroupDiv>
      </HeaderContainer>
    </RootContainer>
  );
};

export default Header;
