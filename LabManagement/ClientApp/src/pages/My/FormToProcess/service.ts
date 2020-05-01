import request from '../../../utils/request';
import { INotifyUpdateParam } from 'umi';

export async function queryMessage() {
  return request('/api/query/msg');
}

export async function queryNotify(){
  return request('/api/query/notify');
}

export async function updateNotify(param: INotifyUpdateParam){
  return request('/api/notify',{
    method: 'PUT',
    data: param
  })
}