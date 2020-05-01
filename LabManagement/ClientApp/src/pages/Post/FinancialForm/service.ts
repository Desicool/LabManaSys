import request from '../../../utils/request';

export async function submitFinancialForm(params: any) {
  return request('/api/form/financial', {
    method: 'POST',
    data: { form: { ...params } },
  });
}
