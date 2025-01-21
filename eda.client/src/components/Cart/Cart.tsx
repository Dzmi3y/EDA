import { useAppContext } from "../../AppProvider";
import { CartItem } from "../../Data/CartItem";

export const Cart = () => {
  const appContext = useAppContext();
  const cartList = appContext.cart;

  return (
    <div style={{ overflow: "auto", height: "400px" }}>
      {cartList.map((p) => (
        <div key={p.product.id}>
          <b />
          <p>{p.product.id}</p>
          <p>{p.product.title}</p>
          <p>{p.count}</p>
          <b />
        </div>
      ))}
    </div>
  );
};
