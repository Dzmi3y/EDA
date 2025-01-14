import styled from "styled-components";
import { motion } from "motion/react";

export const Container = styled.div`
  background: rgba(0, 79, 68, 0.4);
  cursor: progress;
  margin-top: auto;
  position: absolute;
  left: 0px;
  right: 0px;
  top: 0px;
  bottom: 0px;
  border-radius: 10px;
  box-shadow: 0px 0px 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;
export const Description = styled(motion.div)`
  font-weight: 400;
  text-shadow: 2px 2px 0 white;
`;

export const StyledImg = styled.img`
  width: 100px;
  height: 100px;
`;
