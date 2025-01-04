export type ResponseBase<T> = {
  errorMessage: string;
  payload: T;
};
