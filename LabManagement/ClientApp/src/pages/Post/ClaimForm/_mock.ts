// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';

export default {
  'POST  /api/form/claim': (_: Request, res: Response) => {
    res.status(200).send();
  },
};
