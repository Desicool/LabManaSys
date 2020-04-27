import request from 'umi-request';

export async function queryMyChemicals(formid: number) {
  console.log(formid);
  return request('/api/query/user/chemicals', {
    method: 'GET',
    params: {
      formid
    }
  });
}