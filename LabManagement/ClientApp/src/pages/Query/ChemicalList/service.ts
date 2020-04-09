import request from 'umi-request';

export async function queryChemicals() {
  return request('/api/query/chemicals', {
    method: 'GET'
  });
}