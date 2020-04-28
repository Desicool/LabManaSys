import request from 'umi-request';

export async function queryMyChemicals() {
  return request('/api/query/user/chemicals', {
    method: 'GET',
  });
}