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
import testImage from "../../assets/images/testImg.png";

export const ProductCard = () => {
  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };
  return (
    <Container>
      <StyledImage src={testImage} />
      <InfoContainer>
        <Title>Product Name</Title>
        <Price>10$</Price>
        <Description>Description</Description>
        <StyledButton {...buttonAnimationSettings}>Buy</StyledButton>
      </InfoContainer>
    </Container>
  );
};
