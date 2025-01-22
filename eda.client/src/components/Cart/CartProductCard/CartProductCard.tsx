import { CartItem } from "../../../Data/CartItem";
import { Product } from "../../../Data/Product";
import {
  Container,
  Description,
  InfoContainer,
  StyledButton,
  StyledImage,
  Title,
} from "./styles";

export const CartProductCard: React.FC<{ cartItem: CartItem }> = ({
  cartItem,
}) => {
  const cartAnimationSettings = {
    whileHover: { x: -10 },
  };
  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };
  return (
    <Container {...cartAnimationSettings}>
      <StyledImage src={cartItem.product.imageUrl} />
      <Title>
        <b>{cartItem.product.title}</b>
      </Title>
      <InfoContainer>
        <Description>{cartItem.product.description}</Description>
        <StyledButton {...buttonAnimationSettings}>
          Buy {cartItem.product.price}$
        </StyledButton>
      </InfoContainer>
    </Container>
  );
};
