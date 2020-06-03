import request from '../../../utils/request';
import { IChemical } from '@/models/entity';

export async function queryChemicals() {
  return request('/api/query/chemicals', {
    method: 'GET'
  });
}
export async function destroyChemicals(chemical: IChemical) {
  return request('/api/purchase/destroy', {
    method: 'POST',
    data: chemical
  });
}