import request from 'umi-request';

export async function queryMessage() {
  return request('/api/query/msg');
}
