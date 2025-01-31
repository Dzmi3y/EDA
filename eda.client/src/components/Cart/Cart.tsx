import { useAppContext } from "../../AppProvider";
import { Title } from "./CartDialog/styles";
import { CartProductCard } from "./CartProductCard/CartProductCard";
import {
  CartListContainer,
  Container,
  ControlsContainer,
  Message,
  StyledButton,
} from "./styles";

export const Cart = () => {
  const appContext = useAppContext();
  const cartList = appContext.cart;
  const addCount = (id: string) => {
    let newCart = appContext.cart.map((p) => {
      let isAvailable = p.product.count >= p.count + 1;
      if (p.product.id === id && isAvailable) {
        p.count++;
      }
      return p;
    });
    appContext.updateCart(newCart);
  };

  const removeCount = (id: string) => {
    let newCart = appContext.cart.map((p) => {
      let isAvailable = p.count - 1 >= 1;
      if (p.product.id === id && isAvailable) {
        p.count--;
      }
      return p;
    });
    appContext.updateCart(newCart);
  };

  const removeItem = (id: string) => {
    let newCart = appContext.cart.filter((p) => p.product.id !== id);
    appContext.updateCart(newCart);
  };

  const buttonAnimationSettings = {
    whileHover: { scale: 1.1 },
    whileTap: { scale: 0.95 },
  };

  const OrderButtonHandler = () => {
    appContext.updateCart([]);
  };

  return (
    <Container>
      {appContext.cart.length == 0 && <Message>Cart is empty</Message>}
      <CartListContainer>
        {cartList.map((p) => (
          <div key={p.product.id}>
            <CartProductCard
              addCount={async () => addCount(p.product.id)}
              removeCount={async () => removeCount(p.product.id)}
              removeItem={async () => removeItem(p.product.id)}
              cartItem={p}
            />
          </div>
        ))}
      </CartListContainer>
      {appContext.cart.length > 0 && (
        <ControlsContainer>
          <p>
            Total price:{" "}
            {appContext.cart.reduce((accumulator, cartItem) => {
              return accumulator + cartItem.product.price * cartItem.count;
            }, 0)}
            $
          </p>
          <StyledButton
            onClick={OrderButtonHandler}
            {...buttonAnimationSettings}
          >
            Order
          </StyledButton>
        </ControlsContainer>
      )}
    </Container>
  );
};
