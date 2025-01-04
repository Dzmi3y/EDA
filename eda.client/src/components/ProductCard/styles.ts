import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled(motion.div)`
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
  flex-direction: column;
  justify-content: space-between;
  padding-left: 20px;
  padding-right: 20px;
`;
export const Title = styled.p`
  padding-left: 20px;
  padding-right: 20px;
  text-align: center;
  margin-top: 1rem;
  margin-bottom: 1rem;
`;

export const Description = styled.p`
  text-align: center;
  padding-top: 0px;
  padding-bottom: 0px;
  margin-top: 0;
  margin-bottom: 1rem;
  font-size: 1rem;
  min-height: 2rem;
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
  min-width: 150px;
`;
