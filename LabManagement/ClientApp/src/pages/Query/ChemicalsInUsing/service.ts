import request from '../../../utils/request';
import { ISolveFormParam } from '@/models/entity';

export async function queryMyChemicals() {
  return request('/api/query/user/chemicals', {
    method: 'GET',
  });
}
export async function postReturnChemical(param: {cid:number;}) {
  return request('/api/form/claim/return', {
    method: 'POST',
    data: param
  });
}