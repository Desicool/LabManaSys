// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';
import { IFinancialForm } from '@/models/entity';
import moment from 'moment';


const financialform: IFinancialForm =
{
  id: 1,
  uid: 1,
  lid: 2,
  uname: '另一个用户名',
  price: 123,
  state: 'InProcess',
  stime: moment(new Date()).format('YYYY-MM-DD hh:mm:ss'),
}
function getFakeList(req: Request, res: Response) {
  const params = req.query;
  const result = financialform;
  return res.json({
    form: result
  });
}
function approveMock(_: Request, res: Response) {
  res.status(200).send();
}
function rejectMock(_: Request, res: Response) {
  res.status(200).send();
}
export default {
  'GET  /api/form/financial': getFakeList,
  'POST  /api/form/financial/approve': approveMock,
  'POST  /api/form/financial/reject': rejectMock
};
