import { useAppContext } from "../../AppProvider";
import { OrderRequestData } from "../../Data/requests/OrderRequestData";
import { order } from "../../services/ApiService";
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
  const isUserLogged = !!appContext.accessToken;
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

  const OrderButtonHandler = async () => {
    try {
      const requestData: OrderRequestData[] =
        appContext.cart.map<OrderRequestData>((p) => {
          let orderItem: OrderRequestData = {
            id: p.product.id,
            count: p.count,
            price: p.product.price,
          };
          return orderItem;
        });

      const response = await order(requestData);
      appContext.updateCart([]);

      console.log(response.payload);
      console.log(response.errorMessage);
    } catch (e) {
      console.log(e);
    }
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
          {isUserLogged && (
            <StyledButton
              onClick={OrderButtonHandler}
              {...buttonAnimationSettings}
            >
              Order
            </StyledButton>
          )}
          {!isUserLogged && (
            <p>
              Please log in <br />
              to place an order
            </p>
          )}
        </ControlsContainer>
      )}
    </Container>
  );
};
