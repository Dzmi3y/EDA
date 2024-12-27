import styled from "styled-components";

export const Container = styled.div`
  margin-top: 50px;
  padding-top: 10px;
  padding-bottom: 10px;
  height: 100px;
  background-color: var(--primary-color);
  color: var(--secondary-color);
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;
