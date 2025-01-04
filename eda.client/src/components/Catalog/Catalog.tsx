import React from "react";
import { Container } from "./styles";
import { ProductCard } from "../ProductCard/ProductCard";
import { getProducts } from "../../services/ApiService";
export const Catalog = () => {
  var p = getProducts(1, 3);
  p.then((pp) => console.log(pp)).catch((e) => {
    console.log(e);
  });
  return (
    <Container>
      <div>
        Catalog
        <ProductCard />
      </div>
    </Container>
  );
};
