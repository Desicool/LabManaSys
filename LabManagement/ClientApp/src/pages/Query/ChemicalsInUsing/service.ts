import request from '../../../utils/request';

export async function queryMyChemicals() {
  return request('/api/query/user/chemicals', {
    method: 'GET',
  });
}