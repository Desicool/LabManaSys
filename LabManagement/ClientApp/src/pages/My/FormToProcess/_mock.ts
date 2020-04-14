import { IMsgResult } from "umi";
import { IClaimForm, IFinancialForm, IDeclarationForm } from "@/models/entity";
import moment from "moment";

const claimform: IClaimForm[] = [
  {
    id: 1,
    uid: 2,
    uname: '学生1号',
    rtime: moment(new Date()).format('YYYY-MM-DD hh:mm:ss')
  }
]
const financialform: IFinancialForm[] = [
  {
    id: 1,
    uid: 1,
    uname: '另一个用户名',
    price: 123,
  }

]
const declarationform: IDeclarationForm[] = [
{
  id: 1,
  uid: 2,
  uname: '用户名1',
  reason: '为了实验需要'
}
]
const msg: IMsgResult = {
  cform: claimform,
  fform: financialform,
  dform: declarationform,
}
export default {
  'GET  /api/query/msg': msg,
};
