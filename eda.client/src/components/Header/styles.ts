import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled.div`
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;

export const RootContainer = styled.header`
  margin-top: 10px;
  display: flex;
  justify-content: center;
`;

export const MotionButton = styled(motion.button)`
  background: none;
  box-shadow: none;
  padding: 0;
  border: 0;
  margin: 0;
  cursor: pointer;
  height: 50px;
`;

export const IconsGroupDiv = styled.div`
  display: flex;
  justify-content: end;
  gap: 1rem;
  align-items: center;
`;

export const HeaderContainer = styled.div`
  display: grid;
  grid-template-columns: 1.9fr 1fr 1fr 1fr;
`;
