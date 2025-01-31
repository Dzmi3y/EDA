import { Product } from "../../Data/Product";
import {
  Container,
  Description,
  InfoContainer,
  StyledButton,
  StyledImage,
  Title,
} from "./styles";
import { useAppContext } from "../../AppProvider";
import { CartItem } from "../../Data/CartItem";

export const ProductCard: React.FC<Product> = (product) => {
  const appContext = useAppContext();
  const cartAnimationSettings = {
    whileHover: { x: -10 },
  };
  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };

  const buttonClickHandler = () => {
    const existingCartItem = appContext.cart.find(
      (item) => item.product.id === product.id
    );

    let newCart: CartItem[];

    if (existingCartItem) {
      const newCount = Math.min(existingCartItem.count + 1, product.count);
      newCart = appContext.cart.map((item) =>
        item.product.id === product.id ? { ...item, count: newCount } : item
      );
    } else {
      const newCartItem: CartItem = {
        product: product,
        count: 1,
      };
      newCart = [...appContext.cart, newCartItem];
    }

    appContext.updateCart(newCart);
  };

  return (
    <Container {...cartAnimationSettings}>
      <StyledImage src={product.imageUrl} />
      <Title>
        <b>{product.title}</b>
      </Title>
      <InfoContainer>
        <Description>{product.description}</Description>
        <StyledButton onClick={buttonClickHandler} {...buttonAnimationSettings}>
          Buy {product.price}$
        </StyledButton>
      </InfoContainer>
    </Container>
  );
};
