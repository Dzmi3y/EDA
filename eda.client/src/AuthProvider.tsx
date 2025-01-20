import { AuthorizationPayload } from "./Data/payloads/AuthorizationPayload";
import { createContext, useContext, useState, ReactNode } from "react";

export type AuthContextType = AuthorizationPayload & {
  setAuthData: (data: AuthContextType) => void;
  updateAuthData: (data: AuthorizationPayload | null) => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [authData, setAuthData] = useState<AuthContextType>({
    accessToken: "",
    refreshToken: "",
    expiresIn: 0,
    setAuthData: () => {},
    updateAuthData: () => {},
  });

  const updateAuthData = (data: AuthorizationPayload | null) => {
    if (data) {
      const { accessToken, expiresIn, refreshToken } = data;
      const authData: AuthContextType = {
        accessToken,
        expiresIn,
        refreshToken,
        setAuthData: setAuthData,
        updateAuthData: updateAuthData,
      };
      setAuthData(authData);
    } else {
      setAuthData({
        accessToken: "",
        expiresIn: 0,
        refreshToken: "",
        setAuthData: setAuthData,
        updateAuthData: updateAuthData,
      });
    }
  };

  return (
    <AuthContext.Provider
      value={{
        ...authData,
        setAuthData: setAuthData,
        updateAuthData: updateAuthData,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
