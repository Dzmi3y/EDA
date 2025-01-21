import { ThemeProvider } from "styled-components";
import theme from "./theme";
import { Container } from "./styles";
import Header from "./components/Header/Header";
import { Catalog } from "./components/Catalog/Catalog";
import { AppProvider } from "./AppProvider";
function App() {
  return (
    <ThemeProvider theme={theme}>
      <AppProvider>
        <Container>
          <Header />
          <Catalog />
        </Container>
      </AppProvider>
    </ThemeProvider>
  );
}

export default App;
