import { ThemeProvider } from "styled-components";
import theme from "./theme";
import { Container } from "./styles";
import Header from "./components/Header/Header";
import { Catalog } from "./components/Catalog/Catalog";
function App() {
  return (
    <ThemeProvider theme={theme}>
      <Container>
        <Header />
        <Catalog />
      </Container>
    </ThemeProvider>
  );
}

export default App;
