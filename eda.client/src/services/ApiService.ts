import { ProductPayload } from "../Data/ProductPayload";
import { ResponseBase } from "../Data/ResponseBAse";

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
