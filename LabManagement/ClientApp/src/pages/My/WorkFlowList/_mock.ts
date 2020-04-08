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
      state: 'None',
      description: '这是简介' + i.toString(),
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

function postFakeList(req: Request, res: Response) {
  const { /* url = '', */ body } = req;
  // const params = getUrlParams(url);
  const { method, id } = body;
  // const count = (params.count * 1) || 20;
  let result = sourceData || [];

  switch (method) {
    case 'delete':
      result = result.filter((item) => item.id !== id);
      break;
    case 'update':
      result.forEach((item, i) => {
        if (item.id === id) {
          result[i] = { ...item, ...body };
        }
      });
      break;
    case 'post':
      result.unshift({
        ...body,
        id: `fake-list-${result.length}`,
        createdAt: new Date().getTime(),
      });
      break;
    default:
      break;
  }

  return res.json(result);
}

export default {
  'GET  /api/fake_workflow': getFakeList,
  'POST  /api/fake_workflow': postFakeList,
};
