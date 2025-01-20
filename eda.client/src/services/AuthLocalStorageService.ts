import { AuthorizationPayload } from "../Data/payloads/AuthorizationPayload";

const AUTH_KEY = "authData";
export const AuthLocalStorageService = {
  setAuthData: (data: AuthorizationPayload) => {
    localStorage.setItem(AUTH_KEY, JSON.stringify(data));
  },

  getAuthData: (): AuthorizationPayload | null => {
    const data = localStorage.getItem(AUTH_KEY);
    return data ? JSON.parse(data) : null;
  },

  removeAuthData: () => {
    console.log("data removed")
    localStorage.removeItem(AUTH_KEY);
  },

  isAuthDataPresent: (): boolean => {
    return localStorage.getItem(AUTH_KEY) !== null;
  },
};
