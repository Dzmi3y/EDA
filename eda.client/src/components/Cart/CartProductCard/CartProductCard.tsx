import { CartItem } from "../../../Data/CartItem";
import {
  CloseButton,
  Container,
  CountButton,
  Description,
  Header,
  InfoContainer,
  StyledImage,
  Title,
} from "./styles";

export const CartProductCard: React.FC<{ cartItem: CartItem }> = ({
  cartItem,
}) => {
  const buttonAnimationSettings = {
    whileHover: { scale: 1.3 },
    whileTap: { scale: 0.95 },
  };

  const addCount = () => {};

  const removeCount = () => {};

  const removeItem = () => {};

  return (
    <Container>
      <Header>
        <Title>
          <b>{cartItem.product.title}</b>
        </Title>
        <CloseButton onClick={removeItem} {...buttonAnimationSettings}>
          &#x2715;
        </CloseButton>
      </Header>
      <InfoContainer>
        <StyledImage src={cartItem.product.imageUrl} />
        <Description>
          count:{" "}
          <CountButton onClick={removeCount} {...buttonAnimationSettings}>
            -
          </CountButton>
          <b>{cartItem.count}</b>
          <CountButton onClick={addCount} {...buttonAnimationSettings}>
            +
          </CountButton>
        </Description>
        <Description>
          price: <b>{cartItem.product.price}$</b>
        </Description>
      </InfoContainer>
    </Container>
  );
};
