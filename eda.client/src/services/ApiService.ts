import { AuthorizationPayload } from "../Data/payloads/AuthorizationPayload";
import { AuthorizationRequestData } from "../Data/requests/AuthorizationRequestData";
import { ProductPayload } from "../Data/payloads/ProductPayload";
import { RegistrationPayload } from "../Data/payloads/RegistrationPayload";
import { RegistrationRequestData } from "../Data/requests/RegistrationRequestData";
import { ResponseBase } from "../Data/ResponseBase";
import { SignOutRequestData } from "../Data/requests/SignOutRequestData";
import { OrderRequestData } from "../Data/requests/OrderRequestData";

const TIMEOUT: number = 8000;
const BASE_API_URL = "http://localhost:80"; //:5056

export const getProducts = async (
  size: number,
  startIndex: number
): Promise<ResponseBase<ProductPayload>> => {
  const response = await fetch(
    `${BASE_API_URL}/products?size=${size}&startIndex=${startIndex}`
  );
  if (!response.ok) {
    console.log(response.url);
    console.log(response);
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};

export const registration = async (
  requestData: RegistrationRequestData
): Promise<ResponseBase<RegistrationPayload>> => {
  const response = await fetch(`${BASE_API_URL}/Accounts/signup`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(requestData),
    signal: AbortSignal.timeout(TIMEOUT),
  });

  if (!response.ok && response.status !== 400) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};

export const authorization = async (
  requestData: AuthorizationRequestData
): Promise<ResponseBase<AuthorizationPayload>> => {
  const response = await fetch(`${BASE_API_URL}/Accounts/signin`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(requestData),
    signal: AbortSignal.timeout(TIMEOUT),
  });

  if (!response.ok && response.status !== 400) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};

export const signout = async (
  requestData: SignOutRequestData
): Promise<ResponseBase<string>> => {
  const response = await fetch(`${BASE_API_URL}/Accounts/signout`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(requestData),
    signal: AbortSignal.timeout(TIMEOUT),
  });
  if (!response.ok && response.status !== 401) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};

export const order = async (
  requestData: OrderRequestData[],
  bearerToken: string
): Promise<ResponseBase<string>> => {
  console.log(JSON.stringify(requestData));
  console.log(requestData);

  const response = await fetch(`${BASE_API_URL}/orders`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${bearerToken}`,
    },
    body: JSON.stringify(requestData),
    signal: AbortSignal.timeout(TIMEOUT),
  });

  console.log(response);
  console.log(response.status);
  console.log(response.statusText);
  if (!response.ok && response.status !== 400) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};
