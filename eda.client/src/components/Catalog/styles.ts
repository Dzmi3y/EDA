import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  justify-content: center;
  gap: 2rem;
  padding-top: 20px;
  padding-bottom: 40px;
  background-color: var(--primary-color);
  color: var(--secondary-color);
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;
