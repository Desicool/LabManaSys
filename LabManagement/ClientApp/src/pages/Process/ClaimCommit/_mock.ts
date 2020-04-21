// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';
import { IClaimForm, IChemical } from '@/models/entity';
import moment from 'moment';

const titles = [
  '甲苯',
  '硝化纤维素',
  '硝化丙三醇',
  '乙炔（溶于介质的）',
  '氯甲烷',
  '金属钠',
  '硝酸钠',
  '硝酸铅',
  '金属钾',
  '金属钙',
];

const status = [
  '使用中',
  '闲置',
  '已销毁',
];

function fakeList(): IChemical[] {
  const list : IChemical[] = [];
  for (let i = 0; i < 10; i += 1) {
    list.push({
      id: i,
      name: titles[i],
      state: status[i % 3],
      amount: i,
      unitprice: 100,
      unitmeasurement: '升',
    });
  }

  return list;
}
const claimForm: IClaimForm =
{
  id: 1,
  uid: 1,
  lid: 2,
  uname: '一个用户名',
  state: 'InProcess',
  stime: moment(new Date()).format('YYYY-MM-DD hh:mm:ss'),
}
function getFakeList(req: Request, res: Response) {
  const params = req.query;
  const chemicals = fakeList();
  const result = {
    form: claimForm,
    chemicals: chemicals
  }
  return res.json(result);
}
function approveMock(_: Request, res: Response) {
  res.status(200).send();
}
function rejectMock(_: Request, res: Response) {
  res.status(200).send();
}
export default {
  'GET  /api/form/claim': getFakeList,
  'POST  /api/form/claim/approve': approveMock,
  'POST  /api/form/claim/reject': rejectMock
};
