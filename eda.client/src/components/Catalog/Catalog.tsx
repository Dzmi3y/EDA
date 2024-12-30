import React from "react";
import { Container } from "./styles";
import { ProductCard } from "../ProductCard/ProductCard";

export const Catalog = () => {
  return (
    <Container>
      <div>
        Catalog
        <ProductCard />
      </div>
    </Container>
  );
};
