import request from '../../../utils/request';

export async function submitPurchase(workflowid: number) {
  return request('/api/purchase', {
    method: 'GET',
    params: {
      workflowid: workflowid
    },
  });
}
