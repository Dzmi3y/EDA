import { AuthorizationPayload } from "../Data/payloads/AuthorizationPayload";
import { AuthorizationRequestData } from "../Data/requests/AuthorizationRequestData";
import { ProductPayload } from "../Data/payloads/ProductPayload";
import { RegistrationPayload } from "../Data/payloads/RegistrationPayload";
import { RegistrationRequestData } from "../Data/requests/RegistrationRequestData";
import { ResponseBase } from "../Data/ResponseBase";
import { SignOutRequestData } from "../Data/requests/SignOutRequestData";

const TIMEOUT: number = 8000;

export const getProducts = async (
  size: number,
  startIndex: number
): Promise<ResponseBase<ProductPayload>> => {
  const response = await fetch(
    `api/products?size=${size}&startIndex=${startIndex}`
  );
  if (!response.ok) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};

export const registration = async (
  requestData: RegistrationRequestData
): Promise<ResponseBase<RegistrationPayload>> => {
  console.log(JSON.stringify(requestData));
  console.log(requestData);
  const response = await fetch(`api/Accounts/signup`, {
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
  console.log(JSON.stringify(requestData));
  console.log(requestData);

  const response = await fetch(`api/Accounts/signin`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(requestData),
    signal: AbortSignal.timeout(TIMEOUT),
  });
  console.log(response);
  if (!response.ok && response.status !== 400) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};

export const signout = async (
  requestData: SignOutRequestData
): Promise<ResponseBase<string>> => {
  console.log(JSON.stringify(requestData));
  console.log(requestData);

  const response = await fetch(`api/Accounts/signout`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(requestData),
    signal: AbortSignal.timeout(TIMEOUT),
  });
  console.log(response);
  if (!response.ok && response.status !== 400) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};
