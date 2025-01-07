import styled from "styled-components";
import { motion } from "motion/react";

export const StyledForm = styled.form`
  display: flex;
  flex-direction: column;
  gap: 1rem;
`;

export const StyledInput = styled.input`
  border-radius: 2px;
  color: var(--secondary-color);
  /* box-shadow: 2px 2px 10px 2px; */
  border: 1px solid rgba(0, 79, 68, 1);
  padding: 5px;
  margin: 1px 1px;
  outline: none;
  &:focus {
    border: 2px solid rgba(0, 79, 68, 1);
    margin: 0px 0px;
  }
`;

export const Button = styled(motion.button)`
  background: none;
  box-shadow: none;
  padding: 5px;
  border: 0;
  margin: 0;
  cursor: pointer;
  height: 50px;
  color: var(--primary-color);
  background-color: var(--secondary-color);
  box-shadow: 2px 2px 10px 2px var(--secondary-color);
`;

export const Title = styled.h2`
  text-align: center;
  margin: 0;
`;
