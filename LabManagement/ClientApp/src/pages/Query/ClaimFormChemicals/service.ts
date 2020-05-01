import request from '../../../utils/request';

export async function queryClaimDetail(formid: number) {
  return request('/api/form/claim', {
    method: 'GET',
    params:{
      formid
    }
  });
}