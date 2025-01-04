import { Product } from "../Data/Product";

export const getProducts = async (
  size: number,
  startIndex: number
): Promise<Product[]> => {
  const response = await fetch(
    `api/products?size=${size}&startIndex=${startIndex}`
  );
  if (!response.ok) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data;
};
