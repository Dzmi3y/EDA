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
  min-height: 500px;
  min-width: 800px;
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
  position: 4;
`;

export const Title = styled.h2`
  text-align: center;
  margin: 0;
`;

export const SwitchButton = styled.button`
  background: none;
  box-shadow: none;
  border: 0;
  margin: 1rem 0;
  cursor: pointer;
  color: var(--secondary-color);
  font-size: 1rem;
  text-decoration: underline;

  &:hover {
    color: rgba(0, 79, 68, 0.7);
  }
  &:active {
    color: rgba(0, 79, 68, 0.5);
  }
`;
