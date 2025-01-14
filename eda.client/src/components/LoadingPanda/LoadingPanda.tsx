import { motion } from "motion/react";
import PandaLoading from "../../assets/images/loading.gif";
import { Container, Description, StyledImg } from "./styles";

const LoadingPanda: React.FC<{ isVisible?: boolean }> = ({
  isVisible = false,
}) => {
  const text = "Loading...";

  const letterVariants = {
    hidden: { opacity: 0, y: 50 },
    visible: (i: number) => ({
      opacity: 1,
      y: 0,
      transition: {
        delay: i * 0.05,
        duration: 0.5,
        repeat: Infinity,
        repeatDelay: 1,
      },
    }),
  };
  return (
    <>
      {isVisible && (
        <Container>
          <StyledImg src={PandaLoading} alt="loading image" />
          <Description initial="hidden" animate="visible">
            {text.split("").map((letter, index) => (
              <motion.span key={index} custom={index} variants={letterVariants}>
                {letter}
              </motion.span>
            ))}
          </Description>
        </Container>
      )}
    </>
  );
};

export default LoadingPanda;
