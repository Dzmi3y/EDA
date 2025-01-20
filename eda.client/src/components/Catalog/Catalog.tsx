import { Container } from "./styles";
import { ProductCard } from "../ProductCard/ProductCard";
import { getProducts } from "../../services/ApiService";
import { Product } from "../../Data/Product";
import { useEffect, useState } from "react";
import { ResponseBase } from "../../Data/ResponseBase";
import { ProductPayload } from "../../Data/payloads/ProductPayload";

export const Catalog = () => {
  const [products, setProducts] = useState<Product[]>();

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response: ResponseBase<ProductPayload> = await getProducts(4, 1);

        setProducts(response.payload.products);
        console.log(response.payload.products);
      } catch (e) {
        console.log("Error fetching products", e);
      }
    };
    fetchProducts();
  }, []);
  return (
    <Container>
      {Array.isArray(products) &&
        products.map((p) => <ProductCard {...p} key={p.id} />)}
    </Container>
  );
};
