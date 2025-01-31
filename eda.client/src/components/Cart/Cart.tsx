import { useAppContext } from "../../AppProvider";
import { CartProductCard } from "./CartProductCard/CartProductCard";

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

  return (
    <div style={{ overflow: "auto", height: "400px" }}>
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
    </div>
  );
};
