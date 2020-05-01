import request from '../../../utils/request';

export async function queryChemicals() {
  return request('/api/query/chemicals', {
    method: 'GET'
  });
}