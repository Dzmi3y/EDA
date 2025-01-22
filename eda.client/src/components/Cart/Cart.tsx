import { useAppContext } from "../../AppProvider";
import { CartItem } from "../../Data/CartItem";
import { CartProductCard } from "./CartProductCard/CartProductCard";

export const Cart = () => {
  const appContext = useAppContext();
  const cartList = appContext.cart;

  return (
    <div style={{ overflow: "auto", height: "400px" }}>
      {cartList.map((p) => (
        <div key={p.product.id}>
          <CartProductCard cartItem={p} />
        </div>
      ))}
    </div>
  );
};
