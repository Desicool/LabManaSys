import { IMsgResult, INotifyResult } from "umi";
import { IClaimForm, IFinancialForm, IDeclarationForm, IWorkFlow, IChemical } from "@/models/entity";
import moment from "moment";

const claimform: IClaimForm[] = [
  {
    id: 1,
    lid:998,
    uid: 2,
    uname: '学生1号',
    rtime: moment(new Date()).format('YYYY-MM-DD hh:mm:ss'),
    state: 'InProcess',
  }
]
const financialform: IFinancialForm[] = [
  {
    id: 1,
    uid: 1,
    uname: '另一个用户名',
    price: 123,
    state: 'InProcess',
  }

]
const declarationform: IDeclarationForm[] = [
{
  id: 1,
  uid: 2,
  uname: '用户名1',
  reason: '为了实验需要',
  state: 'InProcess',
}
]
const msg: IMsgResult = {
  cform: claimform,
  fform: financialform,
  dform: declarationform,
}
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
const workflows: IWorkFlow[] = [
  {
    id: 2,
    uid: 2,
    state: 'None',
    description: '这是简介x',
    uname: '用户名y',
    startTime: moment(new Date()).toString(),
    chemicals: fakeChemicals(3)
  }
]
const notify: INotifyResult = {
  cform: claimform,
  wf: workflows,
}
export default {
  'GET  /api/query/msg': msg,
  'GET  /api/query/notify': notify,
};
