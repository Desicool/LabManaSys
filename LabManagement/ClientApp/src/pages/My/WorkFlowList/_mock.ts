// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';
import { IWorkFlow, IChemical } from '@/models/entity';
import moment from 'moment';

const status = [
  '使用中',
  '闲置',
  '已销毁',
];
const titles = [
  'Alipay',
  'Angular',
  'Ant Design',
  'Ant Design Pro',
  'Bootstrap',
  'React',
  'Vue',
  'Webpack',
];
function fakeChemicals(count: number): IChemical[] {
  const list : IChemical[] = [];
  for (let i = 0; i < count; i += 1) {
    list.push({
      id: i,
      name: titles[i % count],
      state: status[i % 3],
      amount: i,
      unitprice: 100,
      unitmeasurement: '升',
    });
  }

  return list;
}
function fakeList(count: number): IWorkFlow[] {
  const list : IWorkFlow[] = [];
  for (let i = 0; i < count; i += 1) {
    list.push({
      id: i,
      uid: i * 2,
      state: 'financial',
      description: '这是用于标题的申请简介' + i.toString(),
      uname: '用户名' + i.toString(),
      startTime: moment(new Date()).toString(),
      chemicals: fakeChemicals(3)
    });
  }

  return list;
}

let sourceData: IWorkFlow[] = [];

function getFakeList(req: Request, res: Response) {
  const params = req.query;

  const count = params.count * 1 || 20;

  const result = fakeList(count);
  sourceData = result;
  return res.json(result);
}

export default {
  'GET  /api/query/workflows': getFakeList,
};
