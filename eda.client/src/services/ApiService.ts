import { ProductPayload } from "../Data/ProductPayload";
import { RegistrationPayload } from "../Data/RegistrationPayload";
import { RegistrationRequestData } from "../Data/RegistrationRequestData";
import { ResponseBase } from "../Data/ResponseBase";

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
  });

  if (!response.ok && response.status !== 400) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};
