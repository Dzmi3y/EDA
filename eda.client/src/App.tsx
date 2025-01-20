import { ThemeProvider } from "styled-components";
import theme from "./theme";
import { Container } from "./styles";
import Header from "./components/Header/Header";
import { Catalog } from "./components/Catalog/Catalog";
import { AuthProvider } from "./AuthProvider";
function App() {
  return (
    <ThemeProvider theme={theme}>
      <AuthProvider>
        <Container>
          <Header />
          <Catalog />
        </Container>
      </AuthProvider>
    </ThemeProvider>
  );
}

export default App;
