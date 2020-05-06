import request from '../../../utils/request';
import { IWorkFlow } from '@/models/entity';


export async function queryFakeList() {
  return request('/api/query/workflows');
}