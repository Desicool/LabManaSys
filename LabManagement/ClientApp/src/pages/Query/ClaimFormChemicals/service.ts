import request from 'umi-request';

export async function queryClaimDetail(formid: number) {
  return request('/api/form/claimdetail', {
    method: 'GET',
    params:{
      formid
    }
  });
}