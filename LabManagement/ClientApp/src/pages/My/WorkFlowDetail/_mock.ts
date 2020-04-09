import { Request, Response } from 'express';
import { IFinancialForm, IDeclarationForm, IChemical, IWorkFlow } from '@/models/entity';
import moment from 'moment';
function fakeChemicals(count: number): IChemical[] {
  const list: IChemical[] = [];
  for (let i = 0; i < count; i += 1) {
    list.push({
      id: i,
      name: '瞎编危险品名' + i.toString(),
      state: 'ok',
      amount: i,
      unitprice: 100,
      unitmeasurement: '升',
    });
  }

  return list;
}
const fakeFinancialForm = (workflowid: number) => {
  const list: IFinancialForm[] = [];
  for (let i = 0; i < 3; i++) {
    list.push({
      id: i,
      receiver: '某家公司',
      stime: moment(new Date()).toString(),
      wid: workflowid,
      uid: i * 2,
      uname: '瞎编的名字' + i.toString(),
      lid: i,
      price: (i + 2) * 1425,
      state: 'in lab',
      hid: i + 1,
    })
  }
  return list;
}
const fakeDeclarationForm = (workflowid: number):IDeclarationForm => {
  return {
    id: workflowid,
    stime: moment(new Date()).toString(),
    wid: workflowid,
    uid: workflowid * 2,
    uname: '瞎编的名字' + workflowid.toString(),
    lid: workflowid,
    state: 'None',
    hid: workflowid + 1,
    reason: '瞎编的理由' + workflowid.toString(),
  };
}
const fakeWorkflow = (workflowid: number): IWorkFlow => {
  return {
    id: workflowid,
    uid: 2,
    state: 'None',
    description: '这是简介' + workflowid.toString(),
    uname: '用户名' + workflowid.toString(),
    startTime: moment(new Date()).toString(),
    chemicals: fakeChemicals(3)
  }
}
function getFakeFinancialForms(req: Request, res: Response) {
  const params = req.query;

  const result = fakeFinancialForm(Number.parseInt(params.workflowid));

  return res.json(result);
}
function getFakeDeclarationForms(req: Request, res: Response) {
  const params = req.query;
  const result = fakeDeclarationForm(Number.parseInt(params.workflowid));
  return res.json(result);
}

function getFakeWorkFlow(req: Request, res: Response) {
  const params = req.query;
  const result = fakeWorkflow(Number.parseInt(params.workflowid));
  return res.json(result);
}

export default {
  'GET  /api/query/workflow/financial': getFakeFinancialForms,
  'GET  /api/query/workflow/declaration': getFakeDeclarationForms,
  'GET  /api/query/workflow': getFakeWorkFlow,
};
