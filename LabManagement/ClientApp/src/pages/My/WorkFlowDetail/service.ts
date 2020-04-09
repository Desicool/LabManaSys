import request from 'umi-request';

export async function getFinancialForms(workFlowId: number) {
  return request.get('/api/query/workflow/financial',{
    params:{
      workflowid: workFlowId
    }
  });
}
export async function getDeclarationForms(workFlowId: number) {
  return request.get('/api/query/workflow/declaration',{
    params:{
      workflowid: workFlowId
    }
  });
}
export async function getWorkFlowById(workFlowId: number){
  return request.get('/api/query/workflow',{
    params:{
      workflowid: workFlowId
    }
  })
}