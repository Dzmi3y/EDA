import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled.div`
  font-size: 1.4rem;
  margin-top: 50px;
  padding-top: 10px;
  padding-bottom: 10px;
  background-color: rgba(0, 79, 68, 0.1);
  border-radius: 0px;
  color: var(--secondary-color);
  width: 335px;
  box-shadow: 2px 2px 10px 2px;
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;

export const StyledImage = styled(motion.img)`
  height: 325px;
  width: 325px;
`;
export const InfoContainer = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-left: 20px;
  padding-right: 20px;
`;
export const Title = styled.p``;
export const Price = styled.p`
  width: 100px;
  text-align: center;
`;
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
  width: 100px;
`;
