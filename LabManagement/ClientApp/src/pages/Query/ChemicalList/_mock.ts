// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';
import { IChemical } from '@/models/entity';

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

const status = [
  '使用中',
  '闲置',
  '已销毁',
];

function fakeList(count: number): IChemical[] {
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

let sourceData: IChemical[] = [];

function getFakeList(req: Request, res: Response) {
  const params = req.query;

  const count = params.count * 1 || 20;

  const result = fakeList(count);
  sourceData = result;
  return res.json(result);
}
export default {
  'GET  /api/query/chemicals': getFakeList,
};
