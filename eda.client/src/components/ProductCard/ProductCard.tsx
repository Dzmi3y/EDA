import { Product } from "../../Data/Product";
import {
  Container,
  Description,
  InfoContainer,
  StyledButton,
  StyledImage,
  Title,
} from "./styles";

export const ProductCard: React.FC<Product> = (product) => {
  const cartAnimationSettings = {
    whileHover: { x: -10 },
  };
  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };
  return (
    <Container {...cartAnimationSettings}>
      <StyledImage src={product.imageUrl} />
      <Title>
        <b>{product.title}</b>
      </Title>
      <InfoContainer>
        <Description>{product.description}</Description>
        <StyledButton {...buttonAnimationSettings}>
          Buy {product.price}$
        </StyledButton>
      </InfoContainer>
    </Container>
  );
};
