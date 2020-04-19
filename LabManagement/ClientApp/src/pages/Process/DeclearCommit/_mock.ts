// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';
import { IDeclarationForm, IChemical } from '@/models/entity';
import moment from 'moment';

const data: { form: IDeclarationForm; chemicals: IChemical[] } = {
  form: {
    id: 1,
    state: 'InProcess',
    lid: 999,
    stime: moment(new Date()).format('YYYY-MM-DD hh:mm:ss'),
    reason: '为了xxx实验需要'
  },
  chemicals: [
    {
      id: 1,
      name: 'H2OO',
      wfId: 1,
      amount: 10,
      unitprice: 200,
      unitmeasurement: '升'
    }
  ]
}
function getFakeList(req: Request, res: Response) {
  const params = req.query;
  const result = data;
  return res.json(result);
}
function approveMock(_: Request, res: Response) {
  res.status(200).send();
}
function rejectMock(_: Request, res: Response) {
  res.status(200).send();
}
export default {
  'GET  /api/form/declear': getFakeList,
  'POST  /api/form/declear/approve': approveMock,
  'POST  /api/form/declear/reject': rejectMock
};
