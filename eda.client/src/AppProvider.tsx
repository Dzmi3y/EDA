import { CartItem } from "./Data/CartItem";
import { AuthorizationPayload } from "./Data/payloads/AuthorizationPayload";
import { createContext, useContext, useState, ReactNode } from "react";

export type ContextType = AuthorizationPayload & {
  cart: CartItem[];
  setData: (data: ContextType) => void;
  updateAuthData: (data: AuthorizationPayload | null) => void;
  updateCart: (cart: CartItem[] | null) => void;
};

const Context = createContext<ContextType | undefined>(undefined);

export const AppProvider = ({ children }: { children: ReactNode }) => {
  const [contextData, setContextData] = useState<ContextType>({
    accessToken: "",
    refreshToken: "",
    expiresIn: 0,
    cart: [],
    setData: () => {},
    updateAuthData: () => {},
    updateCart: () => {},
  });

  const updateCart = (data: CartItem[] | null) => {
    if (data) {
      let appData: ContextType = {
        accessToken: contextData.accessToken,
        expiresIn: contextData.expiresIn,
        refreshToken: contextData.refreshToken,
        cart: data,
        setData: setContextData,
        updateAuthData: contextData.updateAuthData,
        updateCart: updateCart,
      };
      setContextData(appData);
    } else {
      let appData: ContextType = {
        accessToken: contextData.accessToken,
        expiresIn: contextData.expiresIn,
        refreshToken: contextData.refreshToken,
        cart: [],
        setData: setContextData,
        updateAuthData: contextData.updateAuthData,
        updateCart: updateCart,
      };
      setContextData(appData);
    }
  };

  const updateAuthData = (data: AuthorizationPayload | null) => {
    if (data) {
      const { accessToken, expiresIn, refreshToken } = data;
      let appData: ContextType = {
        accessToken,
        expiresIn,
        refreshToken,
        cart: contextData.cart,
        setData: setContextData,
        updateAuthData: updateAuthData,
        updateCart: contextData.updateCart,
      };
      setContextData(appData);
    } else {
      setContextData({
        accessToken: "",
        expiresIn: 0,
        refreshToken: "",
        cart: contextData.cart,
        setData: setContextData,
        updateAuthData: updateAuthData,
        updateCart: updateCart,
      });
    }
  };

  return (
    <Context.Provider
      value={{
        ...contextData,
        setData: setContextData,
        updateAuthData: updateAuthData,
        updateCart: updateCart,
      }}
    >
      {children}
    </Context.Provider>
  );
};

export const useAppContext = () => {
  const context = useContext(Context);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
