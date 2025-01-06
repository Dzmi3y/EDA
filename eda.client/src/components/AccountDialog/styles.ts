import styled from "styled-components";
import { motion } from "motion/react";

export const Dialog = styled.dialog`
  background: none;
  border: none;
`;
export const Container = styled.div`
  background: var(--primary-color);
  color: var(--secondary-color);
  border-radius: 0;
  box-shadow: 2px 2px 10px 2px;
  min-height: 400px;
  min-width: 300px;
  padding: 1rem;
  font-family: Steppe;
`;

export const IconButton = styled(motion.button)`
  background: none;
  box-shadow: none;
  padding: 0;
  border: 0;
  margin: 0;
  cursor: pointer;
  height: 50px;
`;

export const CloseButton = styled(motion.button)`
  background: none;
  box-shadow: none;
  padding: 0;
  border: 0;
  margin: 0;
  cursor: pointer;
  height: 50px;
  color: var(--secondary-color);
`;

export const Header = styled.div`
  display: flex;
  justify-content: end;
`;
