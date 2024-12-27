import { ThemeProvider } from "styled-components";
import theme from "./theme";
import { Container } from "./styles";
import elephant from "./assets/images/elephant.svg";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Container>
        <img src={elephant} alt="" />
        <h1>Title</h1>
        <p>Text</p>
      </Container>
    </ThemeProvider>
  );
}

export default App;
