import request from '../../../utils/request';

export async function submitDeclarationForm(params: any) {
  return request('/api/form/declear', {
    method: 'POST',
    data: params,
  }).then(response=>{
    return response;
  });
}
