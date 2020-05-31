// eslint-disable-next-line import/no-extraneous-dependencies
import { Request, Response } from 'express';
import { IChemical } from '@/models/entity';

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

function fakeList(id:number): IChemical[] {
  const list : IChemical[] = [];
  for (let i = 0; i < 10; i += 1) {
    list.push({
      id: i ^ id,
      name: titles[i],
      state: status[i % 3],
      amount: i,
      unitprice: 100,
      unitmeasurement: '升',
      fname: '华东化工厂',
    });
  }

  return list;
}

function getFakeList(req: Request, res: Response) {
  const result = fakeList(2);
  return res.json(result);
}
function fakePost(req: Request, res: Response) {
  return res.status(200).send();
}
export default {
  'GET  /api/query/user/chemicals': getFakeList,
  'POST  /api/form/claim/return': fakePost,
};
