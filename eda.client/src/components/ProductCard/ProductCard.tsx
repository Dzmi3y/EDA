import React from "react";
import {
  Container,
  Description,
  InfoContainer,
  Price,
  StyledButton,
  StyledImage,
  Title,
} from "./styles";

export const ProductCard = () => {
  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };
  return (
    <Container>
      <StyledImage src={"http://localhost:83/images/products/e1.png"} />
      <InfoContainer>
        <Title>Product Name</Title>
        <Price>10$</Price>
      </InfoContainer>
      <InfoContainer>
        <Description>Description</Description>
        <StyledButton {...buttonAnimationSettings}>Buy</StyledButton>
      </InfoContainer>
    </Container>
  );
};
