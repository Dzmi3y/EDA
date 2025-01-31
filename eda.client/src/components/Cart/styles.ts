import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled.div`
  display: flex;
  justify-content: space-between;
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;

export const CartListContainer = styled.div`
  overflow: auto;
  max-height: 400px;
`;

export const ControlsContainer = styled.div`
  margin-right: 20px;
`;

export const StyledButton = styled(motion.button)`
  background: none;
  box-shadow: none;
  color: var(--secondary-color);
  padding: 0;
  border: 1px solid var(--secondary-color);
  margin: 0 0 1rem 0;
  cursor: pointer;
  height: 50px;
  min-width: 250px;
`;

export const Message = styled.h1`
  margin-left: 20px;
`;
