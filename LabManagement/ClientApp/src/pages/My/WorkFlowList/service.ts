import request from 'umi-request';
import { IWorkFlow } from '@/models/entity';

interface ParamsType extends Partial<IWorkFlow> {
  count?: number;
}

export async function queryFakeList(params: ParamsType) {
  return request('/api/query/workflows', {
    params,
  });
}