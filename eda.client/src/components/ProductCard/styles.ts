import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled.div`
  font-size: 1.4rem;
  margin-top: 50px;
  padding-top: 10px;
  padding-bottom: 10px;
  height: 100px;
  background-color: var(--primary-color);
  color: var(--secondary-color);
  width: 280px;
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;

export const StyledImage = styled.img`
  height: 360px;
  width: 279px;
`;
export const InfoContainer = styled.div`
  display: grid;
  grid-template-columns: auto auto;
  padding-left: 5px;
  padding-right: 5px;
`;
export const Title = styled.p``;
export const Price = styled.p``;
export const Description = styled.p``;
export const StyledButton = styled(motion.button)`
  background: none;
  box-shadow: none;
  color: var(--secondary-color);
  padding: 0;
  border: 1px solid var(--secondary-color);
  margin: 0;
  cursor: pointer;
  height: 50px;
`;
