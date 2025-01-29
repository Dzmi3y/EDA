import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled(motion.div)`
  font-size: 1.4rem;
  margin: 20px;
  padding-top: 10px;
  padding-bottom: 10px;
  background-color: rgba(0, 79, 68, 0.1);
  border-radius: 0px;
  color: var(--secondary-color);
  width: 335px;
  box-shadow: 2px 2px 10px 2px;
  display: flex;
  flex-direction: column;
  @media (min-width: ${(props) => props.theme.breakpoints.md}) {
  }
`;

export const StyledImage = styled(motion.img)`
  height: 50px;
  width: 50px;
`;
export const InfoContainer = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-left: 20px;
  padding-right: 20px;
`;
export const Title = styled.p`
  margin-top: 1rem;
  margin-bottom: 1rem;
`;

export const Description = styled.p`
  text-align: center;
  padding-top: 0px;
  padding-bottom: 0px;
  margin-top: 0;
  margin-bottom: 0;
  font-size: 1rem;
`;

export const Header = styled.div`
  padding-left: 20px;
  padding-right: 20px;
  display: flex;
  justify-content: space-between;
  position: 4;
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

export const CountButton = styled(motion.button)`
  background: none;

  font-size: 1.2rem;
  font-weight: 600;
  padding: 0;
  border: 0;
  height: 20px;
  width: 20px;
  border-radius: 50%;
  margin: 0 5px 0 5px;
  cursor: pointer;
  color: var(--secondary-color);
  box-shadow: none;
`;
